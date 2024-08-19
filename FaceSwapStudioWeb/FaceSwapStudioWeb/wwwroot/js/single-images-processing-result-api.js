class SingleImagesProcessingResultApi {
    static #apiUri = location.protocol + '//' + location.host + '/api/SingleImagesProcessingResult'

    static #defaultHeaders = {
        'Accept': 'application/json',
        'Content-Type': 'application/json'
    }

    static add(model) {
        return fetch(SingleImagesProcessingResultApi.#apiUri, {
            method: 'POST',
            headers: this.#defaultHeaders,
            body: JSON.stringify(model)
        })
    }

    static getAll() {
        return fetch(SingleImagesProcessingResultApi.#apiUri, {
            method: 'GET',
            headers: this.#defaultHeaders,
        })
    }

    static get(id) {
        return fetch(`${SingleImagesProcessingResultApi.#apiUri}/${id}`, {
            method: 'GET',
            headers: this.#defaultHeaders,
        })
    }

    static delete(id) {
        return fetch(`${SingleImagesProcessingResultApi.#apiUri}/${id}`, {
            method: 'DELETE',
            headers: this.#defaultHeaders,
        })
    }
}
