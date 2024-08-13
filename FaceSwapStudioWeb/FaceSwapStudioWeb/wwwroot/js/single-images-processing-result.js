const singleImagesProcessingResultUri = 'api/SingleImagesProcessingResult';

class SingleImagesProcessingResult {

    #apiUrl = 'api/SingleImagesProcessingResult'
    #defaultHeaders = {
        'Accept': 'application/json',
        'Content-Type': 'application/json'
    }
    
    // constructor() {
    //     this.apiUrl = 'api/SingleImagesProcessingResult'
    // }
    
    Add(model) {
        return fetch(this.#apiUrl, {
            method: 'POST',
            headers: this.#defaultHeaders,
            body: JSON.stringify(model)
        })
    }
    
    GetAll() {
        return fetch(this.#apiUrl, {
            method: 'GET',
            headers: this.#defaultHeaders,
        })
    }
}

let processing = new SingleImagesProcessingResult();

function Test() {
    let model = {
        startCalculationDateTime: (new Date()).toJSON(),
        // endCalculationDateTime: "2024-08-13T04:19:24.7371127+03:00",
        endCalculationDateTime: (new Date()).toJSON(),
        bodyImage: "1111a",
        faceImage: "2222a",
        swapImage: "3333a",
        enchancedSwapImage: "4444a"
    }

    let model2 = {
        startCalculationDateTime: (new Date()).toJSON(),
        // endCalculationDateTime: "2024-08-13T04:19:24.7371127+03:00",
        endCalculationDateTime: (new Date()).toJSON(),
        bodyImage: "1111b",
        faceImage: "2222b",
        swapImage: "3333b",
        enchancedSwapImage: "4444b"
    }

    processing.Add(model)
        .then(response => {
            processing.Add(model2)
                .then(response => {
                    processing.GetAll()
                        .then(response => response.json())
                        .then(data => data.map(x => console.log(x)));
                })
        })
    
    // processing.Add(model)
    //     // .then(response => response.json())
    //     // .then(data => console.log(JSON.stringify(data)))
    //
    // setTimeout(() => {
    //     processing.Add(model2)
    //         .then(response => response.json())
    //         .then(data => console.log(JSON.stringify(data)));
    //    
    //     }, 1000);
    //
    // processing.Add(model2)
    //     // .then(response => response.json())
    //     // .then(data => console.log(JSON.stringify(data)))
    //
    // setTimeout(() => {
    //     processing.GetAll()
    //         .then(response => response.json())
    //         .then(data => console.log(JSON.stringify(data)))
    //     }, 1000);
    //
    // // processing.GetAll()
    // //     .then(response => response.json())
    // //     .then(data => console.log(JSON.stringify(data)))
    // //     // .then(data => data.map(x => console.log(x)));
}

Test()
