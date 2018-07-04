new Vue({
    el: '#app',
    data: function () {
        return {
            question: {
                questionTitle: '这是一个标题',
                questionContent: '这是内容',
                questionId: 2,
                questionIsHidden: true,
                accountId: 0
            },
            hasFollow: false,
            hasAnswer: false,
            questionLoading: false,
            userId: 1,
            questionId: null,
            isSendingFollow: false,
            loadingFollowStatus: false,
            isSendingHidden: false,
            loadingHiddenStatus: false
        }
    },
    created() {
    },
    methods: {
        getMissingbytype(type, index) {
            this.loadingmissings = true
            let _this = this
            ajaxPost("/Home/getOwnerByType", { type: type }, function (data) {
                if (data.code == 200) {
                    _this.missingOverviewList = data.ownerlist
                }
                _this.loadingmissings = false
            })
        },
        getalltypes() {
            /*ajaxPost("/Home/getOwnerByType", { type: type }, function (data) {
                if (data.code == 200) {
                    _this.missingOverviewList = data.ownerlist
                }

            })*/
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