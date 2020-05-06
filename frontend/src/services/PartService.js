import { Service } from "./Service";

class PartService extends Service {

    getPart(id) {
        return this.get('GetPart?', { partId: id })
    }

    getPartList() {
        return this.get('GetPartList');
    }

    setPart(part) {
        return this.post('SetPart', part);
    }

    setPartPrice(part) {
        return this.post('SetPartPrice', { id: part.id, beszerar: part.beszerar, eladasiar: part.eladasiar});
    }

    removePart(part) {
        return this.post('RemovePart', part)
    }

}

export const partService = new PartService('/api/api/Part/');