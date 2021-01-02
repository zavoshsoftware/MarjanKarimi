function SubmitBlogComment(id) {

 
    var nameVal = $("#commentName").val();
    var emailVal = $("#commentEmail").val();
    var bodyVal = $("#commentBody").val();

    if (nameVal !== "" && emailVal !== "" && bodyVal !== "") {
        $.ajax(
            {
                url: "/BlogComments/SubmitComment",
                data: { name: nameVal, email: emailVal, body: bodyVal, id: id },
                type: "POST"
            }).done(function (result) {
            if (result === "true") {
                $("#errorDivBlog").css('display', 'none');
                $("#SuccessDivBlog").css('display', 'block');
                localStorage.setItem("id", "");
            }
            else if (result === "InvalidEmail") {
                $("#errorDivBlog").html('ایمیل وارد شده صحیح نمی باشد.');
                $("#errorDivBlog").css('display', 'block');
                $("#SuccessDivBlog").css('display', 'none');

            }
        });
    }
    else {
        $("#errorDivBlog").html('تمامی فیلد های زیر را تکمیل نمایید.');
        $("#errorDivBlog").css('display', 'block');
        $("#SuccessDivBlog").css('display', 'none');

    }
}