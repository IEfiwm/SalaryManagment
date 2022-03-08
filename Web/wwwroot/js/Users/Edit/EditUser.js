$('#add-new-familyRole').click(function () {
    var index = document.getElementsByName('familyBox').length;

    var newBox = `<div class="card" id="Box_` + index + `" style="background-color: #ffffff">
        <div class="card-header" name="familyBox"><h6>#`+ index + ` رابطه خویشاوندی</h6></div>
        <div class="card-body">
            <div class="row">
                <input name="AdditionalUserData.index" value="` + index + `" hidden />
                <input name="AdditionalUserData[`+ index + `].Id" value="0" hidden="" type="text" id="AdditionalUserData` + index + `_Id">
                <input name="AdditionalUserData[`+ index + `].ParentRef" value="` + $('#Id').val() + `" hidden="" type="text" >
                <div class="col-6 mb-2">
                    <label for="AdditionalUserData[`+ index + `].FirstName">نام</label>
                    <span class="requiredSpan">*</span>
                    <input name="AdditionalUserData[`+ index + `].FirstName" class="form-control" type="text"  value="" style="background-color: #ffffff!important;color:black;border:1px solid #ced4da" data-val-required="The FirstName field is required."  required >
                </div>
                <div class="col-6 mb-2">
                    <label for="AdditionalUserData[`+ index + `].LastName">نام خانوادگی</label>
                    <span class="requiredSpan">*</span>
                    <input name="AdditionalUserData[`+ index + `].LastName" class="form-control" type="text"  value="" style="background-color: #ffffff!important;color:black;border:1px solid #ced4da" data-val-required="The FirstName field is required."  required >
                </div>
                <div class="col-6 mb-2">
                    <label for="AdditionalUserData[`+ index + `].NationalCode"> کد ملی</label>
                    <span class="requiredSpan">*</span>
                    <input name="AdditionalUserData[`+ index + `].NationalCode" class="form-control" type="text"  value="" style="background-color: #ffffff!important;color:black;border:1px solid #ced4da"
                    data-val="true" data-val-regex="لطفا فیلد را با عدد وارد کنید." data-val-regex-pattern="^(?!(\d)\\1{9})\d{10}$" data-val-required="The IdentityNumber field is required."  required
                    aria-describedby="IdentityNumber-error" aria-invalid="false" />
                </div>
                <div class="col-6 mb-2">
                    <label for="AdditionalUserData[`+ index + `].IdentityNumber"> شماره شناسنامه</label>
                    <span class="requiredSpan">*</span>
                    <input name="AdditionalUserData[`+ index + `].IdentityNumber" class="form-control" type="text"  value="" style="background-color: #ffffff!important;color:black;border:1px solid #ced4da"
                     data-val="true" data-val-regex="لطفا فیلد را با عدد وارد کنید." data-val-regex-pattern="^[0-9]{2,}$" data-val-required="The IdentityNumber field is required."  required
                     aria-describedby="IdentityNumber-error" aria-invalid="false">
                </div>
                <div class="col-6 mb-2">
                    <label for="AdditionalUserData[`+ index + `].Birthday">تاریخ تولد</label>
                    <span class="requiredSpan">*</span>
                    <input name="AdditionalUserData[`+ index + `].Birthday"  class="datepicker form-control" id="AdditionalUserData_` + index + `__Birthday" data-val-required="The FirstName field is required."  required >
                </div>
                <div class="col-6 mb-2">
                    <label class="form-label" >جنسیت</label>
                    <span class="requiredSpan">*</span>
                    <select class="form-control" name="AdditionalUserData[`+ index + `].Gender" style="background-color: #ffffff!important;color:black;border:1px solid #ced4da" data-val-required="The FirstName field is required."  required >
                        <option value="0">زن</option>
                        <option value="1">مرد</option>
                    </select>
                </div>
                <div class="col-6 mb-2">
                    <label class="form-label" >رابطه خویشاوندی</label>
                    <span class="requiredSpan">*</span>
                    <select class="form-control" data-val="true" data-val-required="The FamilyRole field is required." name="AdditionalUserData[`+ index + `].FamilyRole" style="background-color: #ffffff!important;color:black;border:1px solid #ced4da">
                        <option value="1">همسر</option>
                        <option value="2">دختر</option>
                        <option value="3">پسر</option>
                        <option value="4">مادر</option>
                        <option value="5">پدر</option>
                    </select>
                </div>
                <div class="col-6 mb-2">
                    <label class="form-label">آپلود اسناد</label>
                    <br/>
                    <button type="button" onclick="AddDocument(`+ index + `)" class="btn btn-info float-right" title="سند جدید">
                        <svg xmlns="http://www.w3.org/2000/svg" width="16" height="16" fill="currentColor" class="bi bi-file-plus" viewBox="0 0 16 16">
                            <path d="M8.5 6a.5.5 0 0 0-1 0v1.5H6a.5.5 0 0 0 0 1h1.5V10a.5.5 0 0 0 1 0V8.5H10a.5.5 0 0 0 0-1H8.5V6z" />
                            <path d="M2 2a2 2 0 0 1 2-2h8a2 2 0 0 1 2 2v12a2 2 0 0 1-2 2H4a2 2 0 0 1-2-2V2zm10-1H4a1 1 0 0 0-1 1v12a1 1 0 0 0 1 1h8a1 1 0 0 0 1-1V2a1 1 0 0 0-1-1z" />
                        </svg>
                    </button>
                </div>
            </div>
            <div class="row mt-2" id="AdditionalUserData[`+ index + `]_DocumentsBox">
            </div>
        </div>
        <div class="card-footer text-right">
            <button type="button" onclick="RemoveBox('`+ index + `')" class="btn btn-danger">
                <i>حذف</i>
            </button>
        </div>
    </div>`

    $('#mainBox').after(newBox);
    $('#AdditionalUserData_' + index + '__Birthday').persianDatepicker();
});

function RemoveBox(index) {
    $('#Box_' + index).remove();
}


function AddDocument(familyBoxIndex) {
    var index = document.getElementById('AdditionalUserData[' + familyBoxIndex + ']_DocumentsBox').children.length;

    var row = `<div class="col-6 mb-2 form-inline"  id="AdditionalUserData[` + familyBoxIndex + `]_Documents[` + index + `]_Files">
                    <select name="AdditionalUserData[`+ familyBoxIndex + `].Documents[` + index + `].DocumentType" class="form-control ml-0 mr-0 valid"  style="background-color: #ffffff!important;color:black;border:1px solid #ced4da">
                        <option value="1">عکس پرسنلی</option>
                        <option value="2">کارت ملی</option>
                        <option value="3">پشت کارت ملی</option>
                        <option value="4">صفحه اول شناسنامه</option>
                        <option value="5">صفحه دوم شناسنامه</option>
                        <option selected="selected" value="6">صفحه سوم شناسنامه</option>
                        <option value="7">کارت پایان خدمت</option>
                        <option value="8">آخرین مدرک تحصیلی</option>
                        <option value="9">تضامین 1</option>
                        <option value="10">تضامین 2</option>
                        <option value="11">سایر</option>
                    </select>
                    <input type="file" class="ml-2 mr-0" name="AdditionalUserData[` + familyBoxIndex + `].Documents[` + index + `].File"
                                   onchange="SetFileData(this,` + familyBoxIndex + `,` + index + `)" accept="image/*" />
                            <input type="hidden" name="AdditionalUserData[` + familyBoxIndex + `].Documents.index" value="` + index + `" />
                            <input type="hidden" name="AdditionalUserData[` + familyBoxIndex + `].Documents[` + index + `].FullPath" />
                            <input type="hidden" name="AdditionalUserData[` + familyBoxIndex + `].Documents[` + index + `].FileName" />
                            <input type="hidden" name="AdditionalUserData[` + familyBoxIndex + `].Documents[` + index + `].Id" value="0" />
                            <a href="" target="_blank">
                                <img class="form-control mr-3" name="AdditionalUserData[` + familyBoxIndex + `].Documents[` + index + `].Image"  src="" />
                            </a>
                    <button type="button" onclick="RemoveDocument(` + familyBoxIndex + `,` + index + `)" class="btn btn-danger">
                        <i>حذف مدرک</i>
                    </button>
                </div>`;
    document.getElementById('AdditionalUserData[' + familyBoxIndex + ']_DocumentsBox').insertAdjacentHTML("beforeend", row)
}

function RemoveDocument(familyBoxIndex, index) {
    document.getElementById(`AdditionalUserData[` + familyBoxIndex + `]_Documents[` + index + `]_Files`).remove();
}

function SetFileData(File, familyBoxIndex, index) {
    if (File.files && File.files[0]) {
        const allowedExtensions = ['jpg', 'png', 'jpeg', 'bmp'];
        var reader = new FileReader();
        var file = File.files[0];
        var filesize = ((file.size / 1024) / 1024).toFixed(4); // MB
        var fileExtension = file.name.split(".").pop();

        if (!allowedExtensions.includes(fileExtension)) {
            File.value = null;
            alert("file type not allowed");
            return false;
        } else if (filesize > 10) {
            File.value = null;
            alert("file size too large");
            return false;
        }
        else {
            reader.onload = function (e) {
                document.getElementsByName('AdditionalUserData[' + familyBoxIndex + '].Documents[' + index + '].FileName')[0].value = file.name;
                document.getElementsByName('AdditionalUserData[' + familyBoxIndex + '].Documents[' + index + '].Image')[0].src = e.target.result;
                document.getElementsByName('AdditionalUserData[' + familyBoxIndex + '].Documents[' + index + '].Image')[0].parentElement.href = e.target.result;
            }

            reader.readAsDataURL(file);
        }

    }


}