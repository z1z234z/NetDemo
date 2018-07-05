Vue.component('richtext', {
    props: [],
    data: function () {
        return {
            editor:null
        }
    },
    created() {
    },
    mounted() {
        this.editor = window.UE.getEditor('editor')
    },
    methods: {
        geteditor() {
            return this.editor       
        }
    },
    template: `
<div id="richtxt">
    <div id="editor"></div>
</div>`
})
