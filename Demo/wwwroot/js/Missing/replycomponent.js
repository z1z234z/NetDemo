Vue.component('reply', {
    props: ['replyData','isCurrentUser','complete'],
    data: function () {
        return {
            isFeedback: false,
            showComment: false,
            questionId: null,
            commentsList: [],
            newComment: '',
            showAccept:false,
            isLoadingComment: false,
            userinfo: {id:1,name:"123"}
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
    template: `<div class="answer">
    <div>
      <a class="user-info" :href="replyData.account.id">
        <span><img :src="replyData.account.avatarURL"   class="user-avatar"/></span>
        <span>{{ replyData.account.username }}</span>
      </a>
      发布于 <span>{{ replyData.replyDateTime }}</span>
    </div>
    <div class="answer-info" v-html="replyData.replyContent">
    </div>
    <div class="feedback">
      <span @click="toggleComment()"  class="comment"><i class="fa  fa-comments-o fa-lg"></i>
            <span v-show="!showComment">{{ replyData.commentCount || 0 }}条评论</span>
            <span v-show="showComment"> 收起评论 </span>
      </span>
      <span v-if="complete">
        <el-tooltip effect="dark" content="失主已经找到了" placement="top">
          <i style="color: green" class="el-icon-circle-check"></i>
        </el-tooltip>
      </span>
      <span v-else="">
        <el-tooltip effect="dark" content="失主还没找到" placement="top">
          <i class="el-icon-circle-check"></i>
        </el-tooltip>
      </span>
      <span v-if="showAccept">
        <el-tooltip effect="dark" content="该回答解决了我的问题，采纳该回答" placement="top">
          <i @click="acceptAnswer()"  style="color: green" class="fa fa-check"></i>
        </el-tooltip>
      </span>
    </div>
    <div class="commentDetail" v-show="showComment">
      <div class="comment" v-loading.lock="isLoadingComment" element-loading-text="玩命加载中">
    <i class="triangle"></i>
    <ul>
      <li v-for="comment in commentsList" class="commentLi">
        <div class="commentThumbnail">
          <a :href="comment.commenter.id">
            <img :src="comment.commenter.avatarURL"/>
          </a>
        </div>
        <div class="commentMain">
          <a :href="comment.commenter.id">{{ comment.commenter.username }}</a>
          <p class="paragraph"> {{ comment.commentContent }}</p>
          <div class="commentInfo">
            <div class="option">
              <span> {{ comment.commentDateTime  }}</span>
            </div>
          </div>
        </div>
      </li>
    </ul>
    <div class="pages">

    </div>
    <div class="myComment">
      <input type="text" placeholder="请写下您的评论" v-model="newComment"
      @keyup.enter="addNewComment"/>
      <el-button  @click.prevent="addNewComment"  size="small" type="danger" icon="edit">发送评论</el-button>
    </div>
  </div>
    </div>
  </div>`
})