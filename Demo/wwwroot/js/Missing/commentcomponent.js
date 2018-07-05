Vue.component('comment', {
    props: ['answerId'],
    data: function () {
        return {
            commentsList: [],
            newComment: '',
            isLoadingComment: true
        }
    },
    created() {
        this.getallcomments()
        //this.getMissingbytype()
    },
    methods: {
        getallcomments() {
            this.commentsList = [{ commenter: { id: 1, id: "", username: "addf" }, commentContent: "comment content" }, commentDateTime: new Date()]
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
