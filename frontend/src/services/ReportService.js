import {Service} from "./Service";

class ReportService extends Service {

    getWorkerStatistics() {
        return this.get('GetWorkerStatistics')
    }

    getServiceStatistics() {
        return this.get('GetServiceStatistics')
    }

}

export const reportService = new ReportService('/api/api/Report/')