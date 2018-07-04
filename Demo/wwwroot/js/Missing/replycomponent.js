Vue.component('reply', {
    props: ['replyData','isCurrentUser'],
    data: function () {
        return {
            isFeedback: false,
            showComment: false,
            questionId: null
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
      <comment :answerId="replyData.id"></comment>
    </div>
  </div>`
})