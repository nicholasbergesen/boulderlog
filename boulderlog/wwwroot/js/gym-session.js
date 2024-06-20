async function getGymData() {
    const selectedGymId = $("#SessionGym option:selected").val();
    const request = new Request(`/api/gym/${selectedGymId}`);
    let response = await fetch(request);
    let body = await response.json();

    $("#SessionGrade option").remove();
    $("#SessionWall option").remove();

    $.each(body.grade, function (i, v) {
        $("#SessionGrade").append($('<option></option>').val(v.value).html(v.text));
    });

    $.each(body.wall, function (i, v) {
        $("#SessionWall").append($('<option></option>').val(v.text).html(v.text));
    });

    var urlParams = new URLSearchParams(window.location.search);
    const gradeId = urlParams.get("gradeId");
    const wall = urlParams.get("wall");

    if (gradeId) {
        $(`#SessionGrade option[value='${gradeId}']`).attr("selected", "selected");
    }

    if (wall) {
        $(`#SessionWall option[value='${wall}']`).attr("selected", "selected");
    }
}