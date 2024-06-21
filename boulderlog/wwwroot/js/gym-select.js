async function getGymData() {
    const selectedGymId = $("#Gym option:selected").val();
    const request = new Request(`/api/gym/${selectedGymId}`);
    let response = await fetch(request);
    let body = await response.json();

    $("#Grade option").remove();
    $("#Wall option").remove();

    $.each(body.grade, function (i, v) {
        $("#Grade").append($('<option></option>').val(v.id).html(v.colorName));
    });

    $.each(body.wall, function (i, v) {
        $("#Wall").append($('<option></option>').val(v.text).html(v.text));
    });

    var urlParams = new URLSearchParams(window.location.search);
    const gradeId = urlParams.get("gradeId");
    const wall = urlParams.get("wall");

    if (gradeId) {
        $(`#Grade option[value='${gradeId}']`).attr("selected", "selected");
    }

    if (wall) {
        $(`#Wall option[value='${wall}']`).attr("selected", "selected");
    }
}