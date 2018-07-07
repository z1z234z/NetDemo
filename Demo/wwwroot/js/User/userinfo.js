﻿new Vue({
    el: '#app',
    data() {
        return {
            isLoadingData:false,
            pageSize:10,
            total1: 20,
            currentPage1: 1,
            total3: 20,
            currentPage3: 1,
            total2:20,
            currentPage2:1,
            sortParam3: '',
            sortParam1: '',
            sortParam2:'',
            messagelist: [{ senduserinfo: { username: "asasd", avatarURL: "avatarURL", id: "id", account: "account" },content:"私信内容",sourcetitle:"帖子来源",sourceurl:"",sendtime:""}],
            infolist: [{ pageindex: 1, complete: true, completetext: "未找到", title: "标题12",infourl: "/", releasetime: new Date() }],
            allreaply: [{ title: "回复贴的标题",infourl: "/", releasetime: "" }],
            imageUrl: "",
            userInfo: { username: window.localStorage["username"], avatarURL: window.localStorage["avatarURL"], id: window.localStorage["id"], account: window.localStorage["account"] },
            school: "同济大学",
            viewindex:1,
            creditPoint: 3,
            infoPrefix:"我的",
            isCurrentUser:true,
            userId: '',
            userIdIsValidate: true,
            isCheckingUser: false,
            loadingFollowInfo: false,
            followers: 0,
            followed: 0,
            activeIndex: '1',
            avatarUploadShow: false,
            headers: null,
            hasFollow: false,
            isSendingFollow: false,
            isLoadingFollowUserStatus: false,
            isGettingAnswerCount: false,
            isGettingQuestionCount: false,
            answerCount: null,
            questionCount: null,
            showEditProfilePanel: false,
            profile: null,
            isSavingProfile: false,
            loginUser: { avatarURL:""}
        }
    },
    created() {
        //this.userInfo = { username: window.localStorage["username"], avatarURL: window.localStorage["avatarURL"], id: window.localStorage["id"], account: window.localStorage["account"] }
        this.getmessagelist()
    },
    methods: {
        getdetailinfo() {
            let _this = this
            this.isLoadingData = true
            ajaxPost("/Home/GetUserInfo", { account: this.userInfo.account }, function (data) {
                if (data.code == 200) {
                    if (data.userinfo) {
                        _this.userInfo = data.userinfo
                    }
                    else {
                        _this.$message({
                            message: '获取个人信息为空',
                            type: 'error',
                            duration: 2000
                        })
                    }

                }
                else {
                    _this.$message({
                        message: '获取个人信息异常',
                        type: 'error',
                        duration: 2000
                    })
                }
                _this.isLoadingData = false
            })
        },
        getreplylist() {
            let _this = this
            this.isLoadingData = true
            ajaxPost("/Home/GetReplyByUser", { account: this.userInfo.account }, function (data) {
                if (data.code == 200) {
                    if (data.result) {
                        _this.userInfo = data.result
                    }
                    else {
                        _this.$message({
                            message: '获取我的回复为空',
                            type: 'error',
                            duration: 2000
                        })
                    }

                }
                else {
                    _this.$message({
                        message: '获取我的回复异常',
                        type: 'error',
                        duration: 2000
                    })
                }
                _this.isLoadingData = false
            })
        },
        getmessagelist() {
            let _this = this
            this.isLoadingData = true
            ajaxPost("/Home/GetPMByUser", { account: this.userInfo.account }, function (data) {
                if (data.code == 200) {
                    if (data.result) {
                        _this.messagelist = data.result
                    }
                    else {
                        _this.$message({
                            message: '获取我的回复为空',
                            type: 'error',
                            duration: 2000
                        })
                    }

                }
                else {
                    _this.$message({
                        message: '获取我的回复异常',
                        type: 'error',
                        duration: 2000
                    })
                }
                _this.isLoadingData = false
            })
        },
        getinfolist() {
            let _this = this
            this.isLoadingData = true
            ajaxPost("/Home/GetFindingByUser", { account: this.userInfo.account }, function (data) {
                if (data.code == 200) {
                    if (data.result) {
                        _this.infolist = data.result
                    }
                    else {
                        _this.$message({
                            message: '获取我的发帖为空',
                            type: 'error',
                            duration: 2000
                        })
                    }

                }
                else {
                    _this.$message({
                        message: '获取我的发帖异常',
                        type: 'error',
                        duration: 2000
                    })
                }
                _this.isLoadingData = false
            })
        },
        saveProfile() {
            let _this = this
            this.isLoadingData = true
            ajaxPost("/Home/EditProfile", { account: this.userInfo.account, profile: this.profile }, function (data) {
                if (data.code == 200) {
                    if (data.result) {
                        _this.$message({
                            message: '编辑简介成功',
                            type: 'success',
                            duration: 2000
                        })
                    }
                    else {
                        _this.$message({
                            message: '编辑简介失败',
                            type: 'error',
                            duration: 2000
                        })
                    }

                }
                else {
                    _this.$message({
                        message: '编辑简介异常',
                        type: 'error',
                        duration: 2000
                    })
                }
                _this.isLoadingData = false
            })
        },
        changeview(index) {
            this.viewindex = index
            if (index == 1) {
                this.getmessagelist()
            }
            else if (index == 2) {
                this.getdetailinfo()
            }
            else if (index == 3) {
                this.getreplylist()
            }
        },
        handleCurrentChange1() { },
        handleCurrentChange2() { },
        handleCurrentChange3() { },
        handleAvatarSuccess(res, file) {
            this.imageUrl = URL.createObjectURL(file.raw);
        },
        beforeAvatarUpload(file) {
            const isJPG = file.type === 'image/jpeg';
            const isLt2M = file.size / 1024 / 1024 < 2;

            if (!isJPG) {
                this.$message.error('上传头像图片只能是 JPG 格式!');
            }
            if (!isLt2M) {
                this.$message.error('上传头像图片大小不能超过 2MB!');
            }
            return isJPG && isLt2M;
        },
        updateUserInfo() {
            this.userId = this.$route.params.userId
            this.isCheckingUser = true
            this.loadingFollowInfo = true
            this.getUserInfo()
            this.getFollowInfo()
            this.updateFollowUserStatus()
            this.getQuestionAnswerCount()
        },
        getUserInfo() {
            let _this = this
            getUser(this.userId).then((response) => {
                _this.userIdIsValidate = response.status === '200'
                _this.isCheckingUser = false
                _this.user = response.result
            }).catch((e) => {
                Message({
                    message: '请求用户的信息出错，请稍后再试！',
                    type: 'error',
                    duration: 1000
                })
            })
        },
        getFollowInfo() {
            let _this = this
            getFollowInfo(this.userId).then((response) => {
                _this.loadingFollowInfo = false
                _this.followers = response.result.followerCount
                _this.followed = response.result.followedCount
            }).catch((e) => {
                Message({
                    message: '获取用户的关注信息失败，请稍后重试！',
                    type: 'error',
                    duration: 1000
                })
            })
        },
        getQuestionAnswerCount() {
            this.isGettingAnswerCount = true
            let _this = this
            getUserAnswerCount(this.userId).then((response) => {
                _this.isGettingAnswerCount = false
                _this.answerCount = response.result
            }).catch((e) => {
                Message({
                    message: '获取信息失败，请稍后重试！',
                    type: 'error',
                    duration: 1000
                })
            })
            this.isGettingQuestionCount = true
            getUserQuestionCount(this.userId).then((response) => {
                _this.isGettingQuestionCount = false
                _this.questionCount = response.result
            }).catch((e) => {
                Message({
                    message: '获取信息失败，请稍后重试！',
                    type: 'error',
                    duration: 1000
                })
            })
        },
        uploadAvatar() {
            alert("origin")
            let token = this.token
            this.headers = {
                Authorization: token
            }
            this.avatarUploadShow = true
        },
        cropUploadSuccess(response, field) {
            this.$store.dispatch('changeAvatar', response.result)
        },
        followUser() {
            if (!this.hasLogin) {
                bus.$emit('requestLogin')
                return
            }
            this.isSendingFollow = true
            const _this = this
            followUser(this.userId).then((response) => {
                if (response.status === '200') {
                    if (response.result === true) {
                        _this.hasFollow = true
                        Message({
                            message: '关注用户成功！',
                            type: 'success',
                            duration: 1000
                        })
                    }
                }
                _this.isSendingFollow = false
            }).catch((e) => {
                Message({
                    message: '关注用户失败，请稍后重试！',
                    type: 'error',
                    duration: 1000
                })
            })
        },
        unFollowUser() {
            if (!this.hasLogin) {
                bus.$emit('requestLogin')
                return
            }
            this.isSendingFollow = true
            const _this = this
            unFollowUser(this.userId).then((response) => {
                if (response.status === '200') {
                    if (response.result === true) {
                        _this.hasFollow = false
                        Message({
                            message: '取消关注成功',
                            type: 'success',
                            duration: 1000
                        })
                    }
                }
                _this.isSendingFollow = false
            }).catch((e) => {
                Message({
                    message: '取消关注失败，请稍后重试！',
                    type: 'error',
                    duration: 1000
                })
            })
        },
        updateFollowUserStatus() {
            if (this.isCurrentUser || !this.hasLogin) {
                return
            }
            this.isLoadingFollowUserStatus = true
            let _this = this
            hasFollowUser(this.userId).then((response) => {
                _this.isLoadingFollowUserStatus = false
                _this.hasFollow = response.result
            }).catch((e) => {
                Message({
                    message: '加载数据失败，请稍后重试！',
                    type: 'error',
                    duration: 1000
                })
            })
        },
        myFollow() {
            let aimPath = '/user/' + this.userId + '/myFollow'
            this.$router.push({ path: aimPath })
            this.getFollowInfo()
        },
        myFollower() {
            let aimPath = '/user/' + this.userId + '/followers'
            this.$router.push({ path: aimPath })
            this.getFollowInfo()
        },
        saveProfile() {
            this.isSavingProfile = true
            let _this = this
            saveProfile(this.profile).then((response) => {
                if (response.status === '200') {
                    _this.user.profile = response.result.profile
                    _this.isSavingProfile = false
                    _this.showEditProfilePanel = false
                }
            }).catch((e) => {
                Message({
                    message: '保存个人简介失败，请稍后重试！',
                    type: 'error',
                    duration: 1000
                })
                this.showEditProfilePanel = false
            })
        }
    }
    
})