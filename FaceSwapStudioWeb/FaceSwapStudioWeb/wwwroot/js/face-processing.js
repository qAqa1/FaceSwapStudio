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

function swapFace() {
    const request = {
        BodyImage: null,
        FaceImage: null,
        BodyImageFaceIndex: 2
    };

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
        // .then(response => alert(JSON.stringify(response.json())))
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

swapFace()