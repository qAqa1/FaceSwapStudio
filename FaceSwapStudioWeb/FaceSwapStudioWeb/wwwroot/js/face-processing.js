const faceProcessingUri = 'api/FaceProcessing';

// function getData() {
//     fetch(faceProcessingUri)
//         .then(response => response.json())
//         .then(data => _displayItems(data))
//         .catch(error => console.error('Unable to get items.', error));
// }
//
// function _displayItems(data) {
//     alert("ochko")
// }

// getData()

function toBase64 (file) {
    var reader = new FileReader();
    reader.readAsDataURL(file);
    return reader.result;
}

// function getBase64(file) {
//     var reader = new FileReader();
//     reader.readAsDataURL(file);
//     reader.onload = function () {
//         console.log(reader.result);
//     };
//     reader.onerror = function (error) {
//         console.log('Error: ', error);
//     };
// }

// function getBase64(file) {
//     var reader = new FileReader();
//     reader.readAsDataURL(file);
//     reader.onload = function () {
//         console.log(reader.result);
//     };
//     reader.onerror = function (error) {
//         console.log('Error: ', error);
//     };
// }

function swapFace() {
    // var bodyData = new FormData();
    // bodyData.append(document.getElementById('body-input').files[0])
    //
    // var faceData = new FormData();
    // faceData.append(document.getElementById('face-input').files[0])

    console.log(document.getElementById('face-input').files.length);

    // getBase64(document.getElementById('body-input').files[0]);
    // getBase64(document.getElementById('face-input').files[0]);
    
    console.log(toBase64(document.getElementById('body-input').files[0]))
    console.log(toBase64(document.getElementById('face-input').files[0]))
    
    const request = {
        // BodyImage: null,
        // FaceImage: null,
        // BodyImage: document.getElementById('body-input').files[0],
        // FaceImage: document.getElementById('face-input').files[0],
        // BodyImage: new FormData(document.getElementById('body-input').file),
        // FaceImage: new FormData(document.getElementById('face-input').file),
        // BodyImage: document.getElementById('body-input').file,
        // FaceImage: document.getElementById('face-input').file,
        // BodyImage: bodyData,
        // FaceImage: faceData,
        BodyImage: "toBase64(document.getElementById('body-input').files[0])",
        FaceImage: "toBase64(document.getElementById('face-input').files[0])",
        BodyImageFaceIndex: 2
    };
    
    console.log(JSON.stringify(request));

    // const request = {
    //     test: 3,
    // };

    fetch(faceProcessingUri + '/SwapFace', {
        method: 'POST',
        headers: {
            'Accept': 'application/json',
            'Content-Type': 'application/json'
        },
        body: JSON.stringify(request)
    })
        // .then(response => response.json())
        .then(response => console.log(JSON.stringify(response.json())))
        // .then(data => _handleSwapFace(data))
        //.catch(error => console.error('Unable swap face.', error));
}

var toType = function(obj) {
    return ({}).toString.call(obj).match(/\s([a-zA-Z]+)/)[1].toLowerCase()
}

function _handleSwapFace(data) {
    alert(data)
    //alert(data.ProcessingStatus)
    // var status = data.ProcessingStatus;
    // alert(toType(data.SwapImage))
    //console.log("Obama")
}

// swapFace()