class SingleImagesProcessingResultUiController {
    static #processing = new SingleImagesProcessingResultApi();
    static GetAll() {
        SingleImagesProcessingResultUiController.#processing.GetAll()
            .then(response => response.json())
            .then(data => data.map(x => console.log(x)))
    }
}

SingleImagesProcessingResultUiController.GetAll()