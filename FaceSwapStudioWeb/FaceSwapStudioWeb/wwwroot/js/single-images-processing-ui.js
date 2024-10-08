﻿const faceProcessingUri = 'api/FaceProcessing'

function swapFace() {
    var bodyInput = document.getElementById('body-input')
    var faceInput = document.getElementById('face-input')

    if (bodyInput.files.length < 1) {
        console.error("Body image for swap not selected")
        return
    }

    if (faceInput.files.length < 1) {
        console.error("Face image  for swap not selected")
        return
    }

    var bodyReader = new FileReader()
    bodyReader.readAsDataURL(bodyInput.files[0])
    bodyReader.onload = function () {
        var faceReader = new FileReader()
        faceReader.readAsDataURL(faceInput.files[0])
        faceReader.onload = function () {
            var bodyImageArray = bodyReader.result.split(',')
            if (bodyImageArray.length !== 2) {
                console.error("Body image failed extract data")
                return
            }

            var faceImageArray = faceReader.result.split(',')
            if (faceImageArray.length !== 2) {
                console.log("Face image failed extract data")
                return
            }

            _handleSwapFace(bodyImageArray[1], faceImageArray[1])
        }
        faceReader.onerror = function (error) {
            console.error('Unable to load face: ', error)
        }
    }
    bodyReader.onerror = function (error) {
        console.error('Unable to load body: ', error)
    }
}

class LastSwapCache {
    constructor(filenamePrefix, startCalculationDateTime, endCalculationDateTime, bodyImage, faceImage, swapImage, enchancedSwapImage) {
        this.filenamePrefix = filenamePrefix

        this.startCalculationDateTime = startCalculationDateTime
        this.endCalculationDateTime = endCalculationDateTime

        this.bodyImage = bodyImage
        this.faceImage = faceImage
        this.swapImage = swapImage
        this.enchancedSwapImage = enchancedSwapImage
    }
}

var lastSwapCache = new LastSwapCache("", new Date(), new Date().toJSON(), null, null, null, null)

function _handleSwapFace(bodyImage, faceImage) {
    const request = {
        BodyImage: bodyImage,
        FaceImage: faceImage,
        BodyImageFaceIndex: 0
    }

    fetch(faceProcessingUri + '/SwapFace', {
        method: 'POST',
        headers: {
            'Accept': 'application/json',
            'Content-Type': 'application/json'
        },
        body: JSON.stringify(request)
    })
        .then(response => response.json())
        .then(data => {
            var startCalculationDateTime = new Date(Date.parse(data.startCalculationDateTime))
            var endCalculationDateTime = new Date(Date.parse(data.endCalculationDateTime))

            var calculationDurationSeconds = (endCalculationDateTime.getTime() - startCalculationDateTime.getTime()) / 1000

            lastSwapCache = new LastSwapCache(endCalculationDateTime, startCalculationDateTime, endCalculationDateTime, bodyImage, faceImage, data.swapImage, data.enchancedSwapImage)

            var resultContainer = document.getElementById('result-container')

            if (data.processingStatus !== 1) {
                resultContainer.style.display = 'none'
                return
            }

            var swapImageContainer = document.getElementById('swap-image-container')
            var swapImageTag = document.getElementById('swap-image')

            var enchancedSwapImageContainer = document.getElementById('enchanced-swap-image-container')
            var enchancedSwapImageTag = document.getElementById('enchanced-swap-image')

            var showCalculationTimeTag = document.getElementById('show-calculation-time')

            var saveResultButton = document.getElementById('save-result-button')

            if (data.swapImage == null && (data.enchancedSwapImage == null)) {
                resultContainer.style.display = 'none'
                return
            } else
                resultContainer.style.display = 'block'

            if (data.swapImage != null) {
                showCalculationTimeTag.textContent = "Calculation duration: " + calculationDurationSeconds + "s"

                swapImageContainer.style.display = 'block'
                swapImageTag.src = "data:image/png;base64," + data.swapImage
            } else
                swapImageTag.style.display = 'none'

            if (data.enchancedSwapImage != null) {
                enchancedSwapImageContainer.style.display = 'block'
                saveResultButton.style.display = 'block'

                enchancedSwapImageTag.src = "data:image/png;base64," + data.enchancedSwapImage
                enchancedSwapImageTag.style.display = 'block'
            } else {
                enchancedSwapImageContainer.style.display = 'none'
                saveResultButton.style.display = 'none'

                enchancedSwapImageTag.src = ""
                enchancedSwapImageTag.style.display = 'none'
            }

            if (data.swapImage != null)
                enchancedSwapImageTag.scrollIntoView({
                    behavior: 'smooth'
                });
        }
        )
}

function save() {
    let model = {
        startCalculationDateTime: lastSwapCache.startCalculationDateTime.toJSON(),
        endCalculationDateTime: lastSwapCache.endCalculationDateTime.toJSON(),
        bodyImage: lastSwapCache.bodyImage,
        faceImage: lastSwapCache.faceImage,
        swapImage: lastSwapCache.swapImage,
        enchancedSwapImage: lastSwapCache.enchancedSwapImage
    }

    SingleImagesProcessingResultApi.add(model)
        .then(response => response.json())
        .then(() => alert("Face swap result saved"))
        .catch(error => console.error('Unable to add item.', error))
}

function downloadBase64Image(filename, imageData) {
    var a = document.createElement("a")
    a.href = "data:image/png;base64," + imageData
    a.download = filename + ".png"
    a.click()
}

function downloadSelected() {
    var downloadBodyImage = document.getElementById('download-body-image').checked
    var downloadFaceImage = document.getElementById('download-face-image').checked
    var downloadSwapImage = document.getElementById('download-swap-image').checked
    var downloadEnchancedSwapImage = document.getElementById('download-enchanced-swap-image').checked

    var separator = "__"

    var isDesktopSafari = /^((?!chrome|android).)*safari/i.test(navigator.userAgent)
    var isIOS = /^iP/.test(navigator.platform) ||
        /^Mac/.test(navigator.platform) && navigator.maxTouchPoints > 4

    let timeout = 100

    if (isDesktopSafari)
        timeout = 700

    if (isIOS)
        timeout = 10000

    if (downloadBodyImage)
        downloadBase64Image(lastSwapCache.filenamePrefix + separator + "body-image", lastSwapCache.bodyImage)

    setTimeout(() => {
        if (downloadFaceImage)
            downloadBase64Image(lastSwapCache.filenamePrefix + separator + "face-image", lastSwapCache.faceImage)

        setTimeout(() => {
            if (downloadSwapImage)
                downloadBase64Image(lastSwapCache.filenamePrefix + separator + "swap-image", lastSwapCache.swapImage)

            setTimeout(() => {
                if (downloadEnchancedSwapImage)
                    downloadBase64Image(lastSwapCache.filenamePrefix + separator + "enchanced-swap-image", lastSwapCache.enchancedSwapImage)
            }, timeout)
        }, timeout)
    }, timeout)
}