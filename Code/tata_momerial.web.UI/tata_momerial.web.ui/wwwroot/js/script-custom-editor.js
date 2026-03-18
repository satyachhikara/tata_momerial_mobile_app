$(document).ready(function () {
    // Initialize Editor

    $('.textarea-editor').summernote({
        height: 300,
        toolbar: [
            ['style', ['bold', 'italic', 'underline', 'clear']],
            ['para', ['ul', 'ol', 'paragraph']],
            ['table', ['table']],
            ['insert', ['link', 'picture',]],
            ['view', ['codeview']],
        ]
    });
});
