
export class ApiService {
    async ApiPost(url, data) {
        const requestOptions = {
            method: 'POST',
            headers: { 'Content-Type': 'application/json' },
            body: JSON.stringify(data)
        };
        const response = await fetch(url, requestOptions);
        return await response.json();
    }

    async ApiGet(url) {
        const response = await fetch(url);
        return await response.json();        
    }
}

