Vue.component('info-overview', {
    props: ['infoOverview'],
    data: function () {
        return {
            itemHoverIndex: null,
            isSendingHidden: false,
            loadingHiddenStatus: false,
            hasHidden: false
        }
    },
    template: `<div>
        <div>
    <div class="publisherInfo">
      <a :href="infoOverview.id">
        <img class="avatar" :src="infoOverview.avatarURL">
        <span class=".publisher"> {{ infoOverview.username }} </span>
      </a>
    </div>
			<div class="summary">
				<h3>
                <a :href="infoOverview.id">
					{{ infoOverview.questionTitle }}
				</a>
				</h3>
				<div class="types">
				{{ infoOverview.questionSubject }} -> {{ infoOverview.questionCourse }}
				</div>
			</div >
		</div >
    <div class="info">
        <div class="solved-views">
        {{ infoOverview.isAccepted }}
      </div>
    </div>
  </div > `
}) 
new Vue({
    el: '#app',
    data: function () {
        return {
            pageindex:1,
            openindex:[""],
            missingtype:"",
            typedatas: [{
                index: "1", type: "电子产品", children: [
                    { index: "1-1", type: '手机' },
                    { index: "1-2", type: '电脑' }
                ]
            }, {
                index: "2", type: "生活用品", children: [
                    { index: "2-1", type: '雨伞' },
                    { index: "2-2", type: '水杯' }
                ]}]
                ,
            missingorlose:1,
            infoOverviewList: [{
                id: 1,
                avatarURL: "",
                username: "12",
                questionId: 123,
                questionTitle: "title",
                questionSubject: "asdf",
                questionCourse: "asdff",
                hidden: false,
                thumbsUpCount: 1,
                thumbsDownCount: false,
                isAccepted:false

            }],
            loadingmissings: false,
            loadingtypes:false,
            register: 0,
            forget: false,
            registerform: {
                accountreg: '',
                usernamereg: '',
                emailreg: '',
                passwordreg: '',
                passwordAgain: '',
                name: '',
                age: '',
                gender: '',
                school: '',
                phone: '',
                address: ''

            },
            registering: false,
            resetting: false,
            sending: false,
            disableSend: false,
            sendContent: '发送'
        }
    },
    created() {
        this.getalltypes()
        this.getinfobytype()
    },
    methods: {
        radioChange() {
            this.getinfobytype()
        },
        getinfobytype(type, index) {
            this.loadingmissings = true
            var url = ""
            if (this.missingorlose == 1) {
                url = "/Main/getOwnerByType"
            } else {
                url = "/Main/getFinderByType"
            }
            let _this = this
            ajaxPost(url, { type: type,index:index }, function (data) {
                if (data.code == 200) {
                    _this.infoOverviewList = data.infolist
                }
                _this.loadingmissings = false
            })
        },
        getalltypes() {
            var _this = this
            ajaxPost("/Main/getType", {}, function (data) {
                if (data.code == 200) {
                    _this.typedatas = data.typelist
                }

            })
        },
        updateMisssingOverviews() {

        },
        changeview(num) {
            if (this.$refs.loginform !== undefined)
                this.$refs.loginform.resetFields()
            if (this.$refs.registerform !== undefined)
                this.$refs.registerform.resetFields()
            if (this.$refs.forgetform !== undefined)
                this.$refs.forgetform.resetFields()
            this.register = num

        },
        login(ev) {
            let _this = this
            this.$refs.loginform.validate((valid) => {
                if (valid) {
                    _this.logining = true
                    ajaxPost("/Login/testlogin", _this.loginform, function (data) {
                        if (data.comfirm != "true") {
                            alert("用户名或密码出错")
                            _this.logining = false
                        }
                        else {
                            //get转页面
                            _this.logining = false
                            $(location).attr('href', '/Home/index');
                        }
                    })
                }
            })
        },
        userRegister(ev) {
            let _this = this
            this.$refs.registerform.validate((valid) => {
                if (valid) {
                    _this.registering = true
                    ajaxPost("/Login/register", _this.registerform, function (data) {
                        console.log(data.result)
                        console.log(data.code)
                        if (data.code == 200) {
                            if (data.result) {
                                _this.$message({
                                    message: '注册成功!',
                                    type: 'success',
                                    duration: 5000
                                })
                                _this.$refs.registerform.resetFields()

                            }
                            else {
                                _this.$message({
                                    message: '注册失败!',
                                    type: 'error',
                                    duration: 2000
                                })
                                _this.$refs.registerform.resetFields()
                            }

                        }
                        else {
                            _this.$registering = false
                            Message({
                                message: '注册失败!',
                                type: 'error',
                                duration: 2000
                            })
                        }
                    })
                }
            })
        },
        sendMail(ev) {
            let _this = this
            this.$refs.forgetform.validate((valid) => {
                if (valid) {
                    _this.disableSend = true
                    _this.sending = true
                    send(_this.forgetform.mailbox).then((response) => {
                        if (response.status === '201') {
                            Message({
                                message: '发送成功!',
                                type: 'success',
                                duration: 5000
                            })
                            _this.$refs.forgetform.resetFields()
                        } else {
                            Message({
                                message: '发送失败，请稍后重试!',
                                type: 'error',
                                duration: 1000
                            })
                        }
                        _this.sending = false
                    }).catch((e) => {
                        Message({
                            message: '发送失败，请稍后重试!',
                            type: 'error',
                            duration: 1000
                        })
                        _this.sending = false
                    })
                    let sec = 60;
                    for (let i = 0; i <= 60; i++) {
                        window.setTimeout(function () {
                            if (sec !== 0) {
                                _this.sendContent = '(' + sec + ')';
                                sec--;
                            } else {
                                _this.sendContent = '发送'
                                _this.disableSend = false
                            }
                        }, i * 1000)
                    }
                } else {
                    Message({
                        message: '请输入正确邮箱！',
                        type: 'error',
                        duration: 1000
                    })
                }
            })
        },
        resetPassword(ev) {
            if (!this.forgetVerification) {
                Message({
                    message: '请滑动滑块进行验证！',
                    type: 'error',
                    duration: 2000
                })
                return
            }
            let _this = this
            this.$refs.forgetform.validate((valid) => {
                if (valid) {
                    _this.resetting = true
                    reset(_this.forgetform.mailbox, _this.forgetform.password, _this.forgetform.verificationCode).then((response) => {
                        if (response.status === '201') {
                            Message({
                                message: '重置成功!',
                                type: 'success',
                                duration: 5000
                            })
                            _this.$refs.registerform.resetFields()
                        } else {
                            Message({
                                message: '重置失败，请稍后重试!',
                                type: 'error',
                                duration: 1000
                            })
                        }
                        _this.registering = false
                    }).catch((e) => {
                        Message({
                            message: '重置失败，请稍后重试!',
                            type: 'error',
                            duration: 1000
                        })
                        _this.resetting = false
                    })
                }
            })
        },
        resetRegisterForm() {
            const _this = this
            this.$nextTick(function () {
                _this.$refs.registerform.resetFields()
                _this.registerform.password = ''
                _this.registerform.passwordAgain = ''
                _this.initVerificationCode('verification_code_id', function (data) {
                    _this.registerVerification = true
                })
                this.registerVerification = false
            })
        },
        resetLoginForm() {
            const _this = this
            this.$nextTick(function () {
                _this.$refs.loginform.resetFields()
                _this.initVerificationCode('login_verification_code_id', function (data) {
                    _this.loginVerification = true
                })
            })
        },
        resetForgetForm() {
            const _this = this
            this.$nextTick(function () {
                _this.$refs.forgetform.resetFields()
                _this.forgetform.password = ''
                _this.forgetform.passwordAgain = ''
                _this.forgetform.verificationCode = ''
                _this.initVerificationCode('forget_verification_code_id', function (data) {
                    _this.forgetVerification = true
                })
            })
        },
        initVerificationCode(id, callback) {
            const nc = new noCaptcha()
            const nc_appkey = 'FFFF0000000001787D7C'  // 应用标识,不可更改
            const nc_scene = 'register'  // 场景,不可更改
            const nc_token = [nc_appkey, (new Date()).getTime(), Math.random()].join(':')
            const nc_option = {
                renderTo: '#' + id, // 渲染到该DOM ID指定的Div位置
                appkey: nc_appkey,
                scene: nc_scene,
                token: nc_token,
                trans: '{"name1":"code100"}', // 测试用，特殊nc_appkey时才生效，正式上线时请务必要删除；code0:通过;code100:点击验证码;code200:图形验证码;code300:恶意请求拦截处理
                callback: callback
            }
            nc.init(nc_option)
        }
    },
    mounted: function () {
        /*this.register = parseInt(this.$route.query.register)
        if (this.register === 1) {
            this.resetRegisterForm()
        } else if (this.register === 2) {
            this.resetForgetForm()
        } else {
            this.resetLoginForm()
        }*/
    },
    watch: {
        /*'$route'(to, from) {
            this.register = parseInt(this.$route.query.register)
            if (this.register === 1) {
                this.resetRegisterForm()
            } else if (this.register === 2) {
                this.resetForgetForm()
            } else {
                this.resetLoginForm()
            }
        }*/
    }
})