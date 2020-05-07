import { Service } from "./Service";

class WorksheetService extends Service {

    getWorkSheet(id) {
        return this.get('GetWorkSheet?', { worksheetId: id });
    }

    getWorksheetList() {
        return this.get('GetWorkSheets');
    }

    getWorkSheetWithClientId(id) {
        return this.get('GetWorkSheetWithClientId?', { clientId: id });
    }

    setWorksheet(worksheet) {
        return this.post('SetWorkSheet', worksheet);
    }

    setWorksheetDetail(details) {
        return this.post('SetWorkSheetDetail', details);
    }

    removeWorkSheetDetail(id) {
        return this.post('RemoveWorkSheetDetail?', { id: id });
    }

    getWorksheetDetails(id) {
        return this.get('GetWorkSheetDetails?', { worksheetid: id });
    }

    setWorksheetOrder(worksheetOrder) {
        return this.post('SetWorkSheetOrder', worksheetOrder);
    }

    getWorksheetOrders() {
        return this.get('GetWorkSheetOrders');
    }

    getWorksheetOrder(id) {
        return this.get('GetWorkSheetOrders?', { worksheetId: id});
    }

    removeWorksheetOrder(id) {
        return this.post('RemoveWorkSheetOrder', {id: id});
    }

    removeWorksheet(id) {
        return this.post('RemoveWorkSheet', { id: id });
    }

    closeWorksheet(worksheetId, user) {
        return this.post('CloseWorkSheet?worksheetId=' + worksheetId + '&' + 'user=' + user, {});
    }


}

export const worksheetService = new WorksheetService('/api/api/Worksheet/');