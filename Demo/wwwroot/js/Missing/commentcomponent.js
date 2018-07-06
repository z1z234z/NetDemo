Vue.component('comment', {
    props: ['answerId'],
    data: function () {
        return {
            commentsList: [],
            newComment: '',
            isLoadingComment: false
        }
    },
    created() {
        this.getallcomments()
        //this.getMissingbytype()
    },
    methods: {
        getallcomments() {
            let _this = this
            this.isLoadingComment = true
            ajaxPost("/Missing/DetailComment", { id: answerId}, function (data) {
                if (data.code == 200) {
                    if (data.result) {
                        _this.commentsList = data.result
                    }

                }
                else {
                    _this.$message({
                        message: '获取回复异常',
                        type: 'error',
                        duration: 2000
                    })
                }
                _this.replyLoading = false
            })
            
        },
        addNewComment() {
            let _this = this
            postdata = { id: answerId, account: window.localStorage["account"], content: this.newComment }
            console.log(postdata)
            ajaxPost("/Missing/Comment", { id: answerId, account: window.localStorage["account"], content:this.newComment }, function (data) {
                if (data.code == 200) {
                    if (data.result) {
                        _this.$message({
                            message: '发送评论成功',
                            type: 'success',
                            duration: 2000
                        })

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
    template: `
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
              <span> {{ comment.commentDateTime  | moment("ddd, MMM Do YYYY") }}</span>
              <span><i></i> 回复 </span>
              <span><i></i> 赞 </span>
              <span><i></i> 踩 </span>
              <span><i></i> 举报 </span>
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
  </div>`
})
