document.querySelector("#get-image-from-file").addEventListener('click', function () {
    document.querySelector("#file-image").click();
});

document.querySelector("#file-image").addEventListener('change', async function (event) {
    console.log(event)
    let file = event.target.files[0];
    let arrayBuffer = await file.arrayBuffer();
    let base64 = btoa(new Uint8Array(arrayBuffer).reduce((data, byte) => data + String.fromCharCode(byte), ''));
    let imageString = `data:${file.type};base64,${base64}`;
    let canvas = document.querySelector("#canvas");
    var image = new Image();
    image.onload = function () {
        canvas.getContext('2d').drawImage(image, 0, 0, canvas.width, canvas.height);
    };
    image.src = imageString;
    let imageTag = document.querySelector("#imageTag");
    if (imageTag) {
        $(imageTag).addClass("visually-hidden")
    }

    $(canvas).removeClass("placeholder");
    $(canvas).removeClass("visually-hidden");
    

    await createImage(base64, file.type);
});

document.querySelector("#open-camera-modal").addEventListener('click', async function () {
    let stream = await navigator.mediaDevices.getUserMedia({ video: { facingMode: 'environment' }, audio: false });
    video.srcObject = stream;
});

document.querySelector("#capture-image").addEventListener('click', async function () {
    let canvas = document.querySelector("#canvas");
    canvas.getContext('2d').drawImage(video, 0, 0, canvas.width, canvas.height);
    video.srcObject.getTracks()[0].stop();
    $(canvas).removeClass("placeholder");
    let image_data_url = canvas.toDataURL('image/jpeg');
    let imageData = image_data_url.split(",");
    let dataType = imageData[0].substring(imageData[0].indexOf(':', 0) + 1).split(";");

    await createImage(imageData[1], dataType[0]);
});

async function createImage(base64, fileType) {
    const headers = new Headers();
    headers.set('Content-Type', 'application/json');
    headers.set('Accept', 'application/json');

    const request = new Request('/api/images', {
        method: 'POST',
        headers: headers,
        body: JSON.stringify({
            base64: base64,
            fileType: fileType,
            createdAt: Date.now().toString()
        })
    });

    let response = await fetch(request);
    let body = await response.json()
    document.querySelector("#ImageId").value = body.id;
}
