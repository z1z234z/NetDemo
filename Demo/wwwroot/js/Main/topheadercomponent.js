Vue.component('topheader', {
    props: ["showsearch", "infolist", "type", "missorfind", "loadingmissings", "searchtext","pageindex","total"],
    data: function () {
        return {
            userinfo: {},
            hasLogin:false,
            question: '',
            notices: null,
            socket: null
        }
    },
    created() {
        if (window.localStorage["username"]) {
            this.hasLogin = true
            this.userinfo = { username: window.localStorage["username"], avatarURL: window.localStorage["avatarURL"], id: window.localStorage["id"], account: window.localStorage["account"] }
        }
        else {
            this.hasLogin = false
        }
    },
    methods: {
        search() {
            this.searchtext = question
            this.loadingmissings = true
            let _this = this
            ajaxPost("Main/Search", { type: this.type, index: this.pageindex, missingorfind: this.missorfind, searchtext: this.searchtext}, function (data) {
                console.log(data)
                if (data.code == 200) {
                    _this.infolist = data.infolist
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
        logout() {
            this.hasLogin = false
            window.localStorage.removeItem("username")
            window.localStorage.removeItem("avatarURL")
            window.localStorage.removeItem("id")
            window.localStorage.removeItem("account")
        },
        releaseInfo() {
            if (this.hasLogin) {
                $(location).attr('href', '/ReleaseMissing');
            } else {
                $(location).attr('href', '/Login');
            }
        },
        gohome() {
            $(location).attr('href', '/Main/Main');
        }

    },
    template: `<div>
  <el-menu theme="dark" class="el-menu-demo" mode="horizontal">
    <el-menu-item index="0" @click="gohome()">
      <div>
        <img width="150" height="60" src="/images/title.jpg" />
      </div>
    </el-menu-item>
    <el-menu-item index="1" v-if="showsearch==true">
      <el-input 
        @keyup.enter.native="search" v-model="question" placeholder="请输入搜索内容">
      </el-input>
        <i class="el-icon-search" @click="search"></i>
    </el-menu-item>
    <el-menu-item @click="releaseInfo()" index="2-2">
        去发布信息
    </el-menu-item>

    <el-submenu v-if="hasLogin" id="personInfo" index="3">
      <template slot="title"><span><img :src="userinfo.avatarURL" class="top-header-img">
        个人中心</span>
      </template>
      <el-menu-item index="3-1">
          <a :href="'/Home/UserInformation'"> 我的主页</a>
       </el-menu-item>
      <el-menu-item @click="logout()" index="3-3">退出登录</el-menu-item>
    </el-submenu>
    <el-menu-item v-if="!hasLogin" id="login-menu-item" index="6">
      <a :href="'/Login/testlogin'">登录</a>
    </el-menu-item>
  </el-menu>
  </div>`
})