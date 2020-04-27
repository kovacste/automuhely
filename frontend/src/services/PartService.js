import { Service } from "./Service";

class PartService extends Service {

    getPartList() {
        return this.get('GetPartList');
    }

    setPart(part) {
        console.trace()
        return this.post('SetPart', part);
    }

    setPartPrice(part) {
        return this.post('SetPartPrice', part);
    }

}

export const partService = new PartService('/api/api/Part/');