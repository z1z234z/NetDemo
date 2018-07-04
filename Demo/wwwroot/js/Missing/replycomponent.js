Vue.component('reply', {
    props: ['replyData'],
    data: function () {
        return {
            isFeedback: false,
            showComment: false,
            questionId: null
        }
    },
    template: `<div>
        <div>
    <div class="publisherInfo">
      <a :href="missingOverview.id">
        <img class="avatar" :src="missingOverview.avatarURL">
        <span class=".publisher"> {{ missingOverview.username }} </span>
      </a>
    </div>
			<div class="summary">
				<h3>
                <a :href="missingOverview.id">
					{{ missingOverview.questionTitle }}
				</a>
				</h3>
				<div class="types">
				{{ missingOverview.questionSubject }} -> {{ missingOverview.questionCourse }}
				</div>
			</div >
		</div >
    <div class="info">
        <div class="solved-views">
        {{ missingOverview.isAccepted }}
      </div>
    </div>
  </div > `
})