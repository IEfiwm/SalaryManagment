function SetLoading(status, selector) {
    if (status) {
        $(selector).append("<div class='loading'><div class= 'loader'></div ></div >");
    }
    else {
        $(".loading").remove();
    }
}