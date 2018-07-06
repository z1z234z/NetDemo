new Vue({
    el: '#app',
    data: function () {
        return {
            replyLoading: false,
            missingLoading: false,
            id:1,
            isCurrentUser:false,
            userInfo: { username: "shine", avatarURL:""},
            replydialogVisible: false,
            messagedialogVisible: false,
            messagetouser:"",
            missing: {
                missingTitle: '这是一个标题',
                missingContent: '这是内容',
                missingId: 2,
                missingIsHidden: true,
                account: 0,
                complete: true,
                createtime:new Date(),
            },
            allreply: [{
                account: { username: "shine", avatarURL:"",id:3,userhref:"页面还在做，暂时可以不填"},
                replyDateTime: new Date(),
                replyContent: "回答内容",
                commentCount: 3,
                id:4
            }],
            answerNum: -1,
            startIndex: 0,
            onceNum: 5,
            answers: [],
            questionId: '',
            publisherId: '',
            loadingMore: false,
            lastScrollTop: 0,
            isSendingFollow: false,
            sortParamLabel: '默认',
            sortParam: '',
            isHidden: true,
            hasFollow: false,
            hasAnswer: false,
            userId: 1,
            isSendingFollow: false,
            loadingFollowStatus: false,
            isSendingHidden: false,
            loadingHiddenStatus: false
        }
    },
    created() {
        this.getMissingdetail()
    },
    mounted() {
        
    },

    methods: {
        getMissingdetail() {
            let _this = this
            this.missingLoading = true
            ajaxPost("/Missing/Detail", {id:id}, function (data) {
                if (data.code == 200) {
                    if (data.options) {
                        _this.missing = data.result
                    }
                    else {
                        _this.$message({
                            message: '获取结果为空',
                            type: 'error',
                            duration: 2000
                        })
                    }

                }
                else {
                    _this.$message({
                        message: '获取失败',
                        type: 'error',
                        duration: 2000
                    })
                }
                _this.missingLoading = false
            })
        },
        getReplyList() {
            let _this = this
            this.replyLoading = true
            ajaxPost("/Missing/DetailReply", { id: id }, function (data) {
                if (data.code == 200) {
                    if (data.options) {
                        _this.missing = data.result
                    }
                    else {
                       
                    }

                }
                else {
                    _this.$message({
                        message: '获取回复失败',
                        type: 'error',
                        duration: 2000
                    })
                }
                _this.replyLoading = false
            })
        },
        writeMessage() {
            this.messagedialogVisible=true
        },
        writeReply() {
            this.dialogVisible = true
            //this.$refs.replyinput.open()
        },
        submitReply() {
            let _this = this
            ajaxPost("/Missing/Reply", { account: this.userinfo.id, content: this.$refs.richreply.editorcompo.geteditor().getContent() }, function (data) {
                if (data.code == 200) {
                    if (data.result) {
                        _this.$message({
                            message: '发布成功',
                            type: 'success',
                            duration: 2000
                        })
                    }
                    else {
                        _this.$message({
                            message: '发布失败',
                            type: 'error',
                            duration: 2000
                        })
                    }

                }
                else {
                    _this.$message({
                        message: '发布异常',
                        type: 'error',
                        duration: 2000
                    })
                }
            })
        },
        submitMessage() {
            let _this = this
            ajaxPost("/Missing/Message", { account: this.userinfo.id, content: this.messagetouser }, function (data) {
                if (data.code == 200) {
                    if (data.result) {
                        _this.$message({
                            message: '发布成功',
                            type: 'success',
                            duration: 2000
                        })
                    }
                    else {
                        _this.$message({
                            message: '发布失败',
                            type: 'error',
                            duration: 2000
                        })
                    }

                }
                else {
                    _this.$message({
                        message: '发布异常',
                        type: 'error',
                        duration: 2000
                    })
                }
            })

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