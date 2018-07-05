Vue.component('reply', {
    props: ['replyData','isCurrentUser'],
    data: function () {
        return {
            isFeedback: false,
            showComment: false,
            questionId: null,
            commentsList: [],
            newComment: '',
            showAccept:false,
            isLoadingComment: false
        }
    },
    created() {
        //this.getallcomments()
        //this.getMissingbytype()
    },
    methods: {
        getallcomments() {
            this.commentsList = [{ commenter: { id: 1, username: "addf" }, commentContent: "comment content", commentDateTime: new Date()}]
        },
        toggleComment() {
            this.isLoadingComment = true
            this.getallcomments()
            this.showComment = !this.showComment
            this.isLoadingComment = false


        }
    },
    template: `<div class="answer">
    <div>
      <a class="user-info" :href="replyData.account.id">
        <span><img :src="replyData.account.avatarURL"   class="user-avatar"/></span>
        <span>{{ replyData.account.username }}</span>
      </a>
      发布于 <span>{{ replyData.answerDateTime }}</span>
    </div>
    <div class="answer-info" v-html="replyData.answerContent">
    </div>
    <div class="feedback">
      <span @click="toggleComment()"  class="comment"><i class="fa  fa-comments-o fa-lg"></i>
            <span v-show="!showComment">{{ replyData.commentCount || 0 }}条评论</span>
            <span v-show="showComment"> 收起评论 </span>
      </span>
      <span v-if="replyData.accepted">
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
  </div>
    </div>
  </div>`
})