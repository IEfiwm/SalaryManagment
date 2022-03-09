﻿$('.drag').draggable({
    appendTo: 'body',
    helper: 'clone'
});

$('#dropzone').droppable({
    activeClass: 'active',
    hoverClass: 'hover',
    accept: ".drag", // Reject clones generated by sortable
    drop: function (e, ui) {

        var draggedFieldId = $(ui.draggable).prop('id');

        var draggedId = $(ui.draggable).parent().parent().prop('id');
        var draggedBefore = $('#dropzone').children().last().attr('name')

        if (draggedId == draggedBefore) {
            swal('لطفا در بین عوامل از عملگر استفاده کنید.');
            return;
        }

        if (draggedFieldId == 'dynamicNumber') {
            if ($('#dynamicNumber input').val() == '') {
                swal('لطفا فیلد عدد را پر کنید.');
                return;
            }
            draggedFieldId = $('#dynamicNumber input').val();
            $('#dynamicNumber input').val('');
        }

        $elDiv1 = $(`<div class="col-md-3 pl-0"></div>`);
        $elDiv1.append($('<button type="button" title="حذف" class="btn btn-default btn-xs remove"><small class="fa fa-times text-danger"></small></button>')
            .click(function () {
                if ($(this).parent().parent().parent().next().length == 0)
                    $(this).parent().parent().parent().detach();
                else
                    swal(' امکان حذف عملگر/عوامل در بین فرمول وجود ندارد، لطفا از انتها اقدام به حذف کنید');
            })
        );

        ui.draggable.draggable({ disabled: true });
        setTimeout(function () {
            $(ui.draggable).draggable("destroy");
        }, 0);

        if (draggedId == 'CalcProp') {
            var $el = $(`<div class="col-md-2" name="${draggedId}"><input type="hidden" value="[${draggedFieldId}]" name="RuleList" /></div>`);
            var $elDiv = $('<div class="drop-item row"><div class="col-md-9 pr-1"><label class="pt-2">' + draggedFieldId + '</label></div></div>');
            $elDiv.append($elDiv1);
            $el.append($elDiv);
            $(this).append($el);
        }
        else {
            var $el = $(`<div class="col-md-1" name="${draggedId}"><input type="hidden" value="${draggedFieldId}" name="RuleList" /></div>`);
            var $elDiv = $('<div class="drop-item row"><div class="col-md-3 pr-3"><h5 class="pt-2">' + draggedFieldId + '</h5></div></div>');
            $elDiv.append($elDiv1);
            $el.append($elDiv);
            $(this).append($el);
        }


    }
})