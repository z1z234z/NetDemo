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
      <a :href="infoOverview.infourl">
        <img class="avatar" :src="infoOverview.avatarURL">
        <span class=".publisher"> {{ infoOverview.username }} </span>
      </a>
    </div>
			<div class="summary">
				<h3>
                <a :href="infoOverview.infourl">
					{{ infoOverview.infoTitle }}
				</a>
				</h3>
				<div class="types">
				{{ infoOverview.fatherType }} -> {{ infoOverview.type }}
				</div>
			</div >
		</div >
    <div class="info">
        <div class="solved-views">
        {{ infoOverview.completetext }}
      </div>
    </div>
  </div > `
}) 
new Vue({
    el: '#app',
    data: function () {
        return {
            searchtext:"",
            total:0,
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
            infoOverviewList: [],
            completetext:"",
            loadingmissings: false,
            loadingtypes:false,
            register: 0,
            forget: false,
            registering: false,
            resetting: false,
            sending: false,
            disableSend: false,
            sendContent: '发送'
        }
    },
    created() {
        this.getalltypes()
        this.getinfo()
    },
    methods: {
        radioChange() {
            this.pageindex = 1
            this.getinfo()
        },
        getinfo() {
            this.loadingmissings = true
            let _this = this
            ajaxPost("/Main/Search", { type: this.missingtype, index: this.pageindex, missingorfind: this.missingorlose, searchtext: this.searchtext }, function (data) {
                console.log(data)
                if (data.code == 200) {
                    _this.infoOverviewList = data.infolist
                    _this.total = data.total
                }
                else {
                    _this.$message({
                        message: '获取失败!',
                        type: 'error',
                        duration: 2000
                    })
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
        pagechange() {
            getinfobytype(this.missingtype, this.pageindex)
        },
        typechange(type, index) {
            this.missingtype = type
            this.pageindex = 1
            this.getinfo()
        },
        updateMisssingOverviews() {

        },
       
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