export function parseWebAPIErrors(response: any): string[] {
    const result: string[] = [];

    if (response.error){
        if (typeof response.error === 'string'){
            result.push(response.error);
    }

        return result;
}
}
