function SetLoading(status, selector) {
    if (status) {
        $(selector).append("<div class='loading'><div class= 'loader'></div ></div >");
    }
    else {
        $(".loading").remove();
    }
}
function setInputFilter(textbox, inputFilter) {
    ["input", "keydown", "keyup", "mousedown", "mouseup", "select", "contextmenu", "drop"].forEach(function (event) {
        textbox.addEventListener(event, function () {
            if (inputFilter(this.value)) {
                this.oldValue = this.value;
                this.oldSelectionStart = this.selectionStart;
                this.oldSelectionEnd = this.selectionEnd;
            } else if (this.hasOwnProperty("oldValue")) {
                this.value = this.oldValue;
                this.setSelectionRange(this.oldSelectionStart, this.oldSelectionEnd);
            } else {
                this.value = "";
            }
        });
    });
}
$.each(document.getElementsByClassName("floatTextBox"), function (index, item) {
    setInputFilter(item, function (value) {
        return /^-?\d*[.,]?\d*$/.test(value);
    });
});
$.each(document.getElementsByClassName("intTextBox"), function (index, item) {
    setInputFilter(item, function (value) {
        return /^-?\d*$/.test(value);
    });
});
