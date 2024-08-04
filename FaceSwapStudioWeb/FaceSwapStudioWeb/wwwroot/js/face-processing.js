﻿const faceProcessingUri = 'api/FaceProcessing';

function swapFace() {
    var bodyInput = document.getElementById('body-input');
    var faceInput = document.getElementById('face-input');

    if (bodyInput.files.length < 1) {
        console.log("Body image for swap not selected");
        return;
    }

    if (faceInput.files.length < 1) {
        console.log("Face image  for swap not selected");
        return;
    }

    var bodyReader = new FileReader();
    bodyReader.readAsDataURL(bodyInput.files[0]);
    bodyReader.onload = function () {
        var faceReader = new FileReader();
        faceReader.readAsDataURL(faceInput.files[0]);
        faceReader.onload = function () {
            // console.log(bodyReader.result);
            // console.log(faceReader.result);

            // alert(bodyReader.result.split(',').length);

            var bodyImageArray = bodyReader.result.split(',');
            if (bodyImageArray.length !== 2) {
                console.log("Body image failed extract data");
                return;
            }

            var faceImageArray = faceReader.result.split(',');
            if (faceImageArray.length !== 2) {
                console.log("Face image failed extract data");
                return;
            }

            _handleSwapFace(bodyImageArray[1], faceImageArray[1])
        };
        faceReader.onerror = function (error) {
            console.log('Unable to load face: ', error);
        };
    };
    bodyReader.onerror = function (error) {
        console.log('Unable to load body: ', error);
    };
}

class LastSwapCache {
    constructor(filenamePrefix, faceImage, bodyImage, swapImage, enchancedSwapImage) {
        this.filenamePrefix = filenamePrefix;

        this.faceImage = faceImage;
        this.bodyImage = bodyImage;
        this.swapImage = swapImage;
        this.enchancedSwapImage = enchancedSwapImage;
    }
}

var lastSwapCache = new LastSwapCache("", null, null, null, null);

function _handleSwapFace(bodyImage, faceImage) {
    const request = {
        BodyImage: bodyImage,
        FaceImage: faceImage,
        BodyImageFaceIndex: 0
    };

    // console.log(JSON.stringify(request));

    fetch(faceProcessingUri + '/SwapFace', {
        method: 'POST',
        headers: {
            'Accept': 'application/json',
            'Content-Type': 'application/json'
        },
        body: JSON.stringify(request)
    })
        .then(response => response.json())
        // .then(data => alert(JSON.stringify(data)));
        .then(data => {
                // alert(data.processingStatus);

                var separator = "-";

                var m = new Date();
                var dateString =
                    m.getUTCFullYear() + separator +
                    ("0" + (m.getUTCMonth() + 1)).slice(-2) + separator +
                    ("0" + m.getUTCDate()).slice(-2) + "_" +
                    ("0" + m.getUTCHours()).slice(-2) + separator +
                    ("0" + m.getUTCMinutes()).slice(-2) + separator +
                    ("0" + m.getUTCSeconds()).slice(-2);

                console.log(dateString);

                lastSwapCache = new LastSwapCache(dateString, bodyImage, faceImage, data.swapImage, data.enchancedSwapImage);

                var resultContainer = document.getElementById('result-container');

                if (data.processingStatus !== 1) {
                    resultContainer.style.display = 'none';
                    return;
                }

                var swapImageContainer = document.getElementById('swap-image-container');
                var swapImageTag = document.getElementById('swap-image');

                var enchancedSwapImageContainer = document.getElementById('enchanced-swap-image-container');
                var enchancedSwapImageTag = document.getElementById('enchanced-swap-image');

                if (data.swapImage == null && (data.enchancedSwapImage == null)) {
                    resultContainer.style.display = 'none';
                    return;
                } else
                    resultContainer.style.display = 'block';

                if (data.swapImage != null) {
                    swapImageContainer.style.display = 'block';
                    swapImageTag.src = "data:image/png;base64," + data.swapImage;

                    // var a = document.createElement("a"); //Create <a>
                    // a.href = "data:image/png;base64," + data.swapImage; //Image Base64 Goes here
                    // a.download = "Image.png"; //File name Here
                    // a.click(); //Downloaded file
                } else
                    swapImageTag.style.display = 'none';

                if (data.enchancedSwapImage != null) {
                    enchancedSwapImageContainer.style.display = 'block';
                    enchancedSwapImageTag.src = "data:image/png;base64," + data.enchancedSwapImage;

                    enchancedSwapImageTag.style.display = 'block';
                } else
                    enchancedSwapImageTag.style.display = 'none';

                if (data.swapImage != null)
                    window.scrollTo(0, document.body.scrollHeight);
            }
        );
}

function downloadBase64Image(filename, imageData) {
    var a = document.createElement("a"); //Create <a>
    a.href = "data:image/png;base64," + imageData; //Image Base64 Goes here
    a.download = filename + ".png"; //File name Here
    a.click(); //Downloaded file
}

function downloadSelected() {
    var downloadBodyImage = document.getElementById('download-body-image').checked;
    var downloadFaceImage = document.getElementById('download-face-image').checked;
    var downloadSwapImage = document.getElementById('download-swap-image').checked;
    var downloadEnchancedSwapImage = document.getElementById('download-enchanced-swap-image').checked;

    var separator = "__";

    var isDesctopSafari = /^((?!chrome|android).)*safari/i.test(navigator.userAgent);
    var isIOS = /^iP/.test(navigator.platform) ||
        /^Mac/.test(navigator.platform) && navigator.maxTouchPoints > 4;

    let timeout = 100;

    if (isDesctopSafari)
        timeout = 700;

    if (isIOS)
        timeout = 10000;

    if (downloadBodyImage)
        downloadBase64Image(lastSwapCache.filenamePrefix + separator + "body-image", lastSwapCache.bodyImage);

    setTimeout(() => {
        if (downloadFaceImage)
            downloadBase64Image(lastSwapCache.filenamePrefix + separator + "face-image", lastSwapCache.faceImage);

        setTimeout(() => {
            if (downloadSwapImage)
                downloadBase64Image(lastSwapCache.filenamePrefix + separator + "swap-image", lastSwapCache.swapImage);

            setTimeout(() => {
                if (downloadEnchancedSwapImage)
                    downloadBase64Image(lastSwapCache.filenamePrefix + separator + "enchanced-swap-image", lastSwapCache.enchancedSwapImage);
            }, timeout);
        }, timeout);
    }, timeout);
}