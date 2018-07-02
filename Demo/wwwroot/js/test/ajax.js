let ajaxPost = function (url, result, success) {
    $.ajax({
        headers: {
            'Accept': 'application/json',
            'Content-Type': 'application/x-www-form-urlencoded'
        },
        type: 'POST',
        url: url,
        data: result,
        success: success
    });
};
