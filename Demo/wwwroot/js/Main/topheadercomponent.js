Vue.component('topheader', {
    props: [],
    data: function () {
        return {
            isFeedback: false,
            showComment: false,
            questionId: null,
            commentsList: [],
            newComment: '',
            showAccept: false,
            isLoadingComment: false,
            userinfo: { id: 1, name: "123" }
        }
    },
    created() {
    },
    methods: {
        getallcomments() {
            this.isLoadingComment = true
            let _this = this
            ajaxPost("/Missing/DetailComment", { id: this.replyData.id }, function (data) {
                if (data.code == 200) {
                    if (data.result) {
                        _this.commentsList = data.result
                        _this.replyData.commentCount = data.result.length

                    }
                    else {

                    }

                }
                else {
                    _this.$message({
                        message: '获取评论失败',
                        type: 'error',
                        duration: 2000
                    })
                }
                this.isLoadingComment = false
            })

        },
        toggleComment() {
            this.isLoadingComment = true
            this.getallcomments()
            this.showComment = !this.showComment
            this.isLoadingComment = false
        },
        addNewComment() {
            let _this = this
            postdata = { account: window.localStorage["account"], id: this.replyData.id, content: this.newComment }
            console.log(postdata)
            ajaxPost("/Missing/Comment", { account: window.localStorage["account"], id: this.replyData.id, content: this.newComment }, function (data) {
                if (data.code == 200) {
                    if (data.result) {
                        _this.$message({
                            message: '发送评论成功',
                            type: 'success',
                            duration: 2000
                        })
                        _this.newComment = ''
                        _this.getallcomments()

                    } else {
                        _this.$message({
                            message: '发送评论失败',
                            type: 'error',
                            duration: 2000
                        })
                    }

                }
                else {
                    _this.$message({
                        message: '发送评论失败',
                        type: 'error',
                        duration: 2000
                    })
                }
            })
        }
    },
    template: `<div>
  <el-menu theme="dark" class="el-menu-demo" mode="horizontal">
    <el-menu-item index="0">
      <router-link to="/">
        <img width="150" height="45" src="./img/logo.png" />
      </router-link>
    </el-menu-item>
    <el-menu-item index="1" >
      <el-input
        @keyup.enter.native="search" v-model="question" icon="search" :on-icon-click="search" placeholder="请输入搜索内容">
      </el-input>
    </el-menu-item>
    <el-menu-item v-if="hasLogin&&user.roles[0].id===2" @click="$router.push({ path: `/ admin / allQuestion` })" index="2-1">
        管理
    </el-menu-item>
    <el-menu-item v-else @click="postQuestion()" index="2-2">
        去提问
    </el-menu-item>

    <el-submenu v-if="hasLogin" id="personInfo" index="3">
      <template slot="title"><span><img :src="user.avatarURL" class="top-header-img">
        个人中心</span>
      </template>
      <el-menu-item v-if="user.roles[0].id===1" @click="$router.push({ path: `/ user / ${ user.id }` })" index="3-1">
          我的主页
       </el-menu-item>
      <el-menu-item v-else @click="$router.push({ path: `/ admin / allQuestion` })" index="3-2">
          系统管理
      </el-menu-item>
      <el-menu-item @click="logout" index="3-3">退出登录</el-menu-item>
    </el-submenu>
    <el-submenu  v-if="hasLogin" id="notification" index="4">
      <template v-if="user.roles[0].id==2" style="" slot="title">
        <el-badge style="margin-right: 20px" :value="1" :max="99" class="item">
          <i class="fa fa-bell-o fa-lg"></i>
        </el-badge>
      </template>
      <template v-else style="" slot="title">
        <el-badge style="margin-right: 20px" :value="noticeNum" :max="99" class="item">
          <i class="fa fa-bell-o fa-lg"></i>
        </el-badge>
      </template>
        <el-menu-item v-if="this.user.roles[0].id==2">
          你是本系统的管理员
        </el-menu-item>
        <el-menu-item v-else-if="noticeNum <= 0" style="text-align: center; word-wrap: break-word" index="4-1">
          暂时没有收到任何提醒
        </el-menu-item>
        <el-menu-item v-if="this.user.roles[0].id==1&&noticeNum >0"@click="clearNotice()" index="4-1" style="color: red; text-align: center">
          <i class="el-icon-delete2"></i><span >清空提醒</span>
        </el-menu-item>
        <span v-for="(notice, index) in notices" :key="notice.id">
        <el-menu-item v-if="notice.type == 0" @click="readAnswer(index, notice)" class="notice" index="4-2">
          <span  class="user">{{ notice.answer.account.username }}</span>
          回答了
          <el-tooltip :content="notice.question.questionTitle" placement="top">
          <span  class="questionTitle">
            " {{ notice.question.questionTitle }} "
          </span>
        </el-tooltip>
          <span class="answerTime">
            {{ $moment(notice.answer.answerDateTime).fromNow() }}
          </span>
        </el-menu-item>
        <el-menu-item v-else-if="notice.type == 1" class="notice" :key="notice.id" index="4-2">
          问题
          <el-tooltip :content="notice.question.questionTitle" placement="top">
          <span  class="questionTitle">
            " {{ notice.question.questionTitle }} "
          </span>
          </el-tooltip>
          <span class="answerTime">
            已被屏蔽！
          </span>
        </el-menu-item>
        <el-menu-item v-else-if="notice.type == 2" class="notice" :key="notice.id" index="4-2">
          你在
          <el-tooltip :content="notice.question.questionTitle" placement="top">
          <span  class="questionTitle">
            " {{ notice.question.questionTitle }} "
          </span>
          </el-tooltip>
          <span class="answerTime">
            中的回答已被屏蔽！
          </span>
        </el-menu-item>
          <el-menu-item v-else-if="notice.type == 3" class="notice" :key="notice.id" index="4-2">
          你在
          <el-tooltip :content="notice.question.questionTitle" placement="top">
          <span  class="questionTitle">
            " {{ notice.question.questionTitle }} "
          </span>

          </el-tooltip>
            <span class="answerTime">
            中的回答被采纳啦！
          </span>
        </el-menu-item>
                  <el-menu-item v-else-if="notice.type == 4" class="notice" :key="notice.id" index="4-2">
          问题
          <el-tooltip :content="notice.question.questionTitle" placement="top">
          <span  class="questionTitle">
            " {{ notice.question.questionTitle }} "
          </span>
          </el-tooltip>
          <span class="answerTime">
            已取消屏蔽！
          </span>
        </el-menu-item>
                  <el-menu-item v-else-if="notice.type == 2" class="notice" :key="notice.id" index="4-2">
          你在
          <el-tooltip :content="notice.question.questionTitle" placement="top">
          <span  class="questionTitle">
            " {{ notice.question.questionTitle }} "
          </span>
          </el-tooltip>
          <span class="answerTime">
            中的回答已取消屏蔽！
          </span>
        </el-menu-item>
        </span>
    </el-submenu>
    <el-menu-item @click="$router.push('/login?register=true')" v-if="!hasLogin" id="register-menu-item" index="5">
      注册
    </el-menu-item>
    <el-menu-item @click="$router.push('/login')"  v-if="!hasLogin" id="login-menu-item" index="6">
      登录
    </el-menu-item>
  </el-menu>
    <login-dialog  ref="loginDialog" @onLogin="loginSuccess"></login-dialog>
  </div>`
})