const faceProcessingUri = 'api/FaceProcessing';

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
                } else
                    swapImageTag.style.display = 'none';

                if (data.enchancedSwapImage != null) {
                    enchancedSwapImageContainer.style.display = 'block';
                    enchancedSwapImageTag.src = "data:image/png;base64," + data.enchancedSwapImage;
                } else
                    enchancedSwapImageTag.style.display = 'none';

                if (data.swapImage != null)
                    window.scrollTo(0, document.body.scrollHeight);
            }
        );
}
