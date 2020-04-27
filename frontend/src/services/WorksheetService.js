import { Service } from "./Service";

class WorksheetService extends Service {

    getWorksheetList() {
        return this.get('GetWorkSheets');
    }

    setWorksheet(part) {
        return this.post('SetWorkSheet', part);
    }

    setWorksheetDetail(details) {
        return this.post('SetWorkSheetDetail', details);
    }

    removeWorkSheetDetail(id) {
        return this.post('RemoveWorkSheetDetail', { id: id });
    }

    getWorksheetDetails(id) {
        return this.get('GetWorkSheetDetails', { worksheetid: id })
    }
}

export const worksheetService = new WorksheetService('/api/api/Worksheet/');