import {Service} from "./Service";

class ReportService extends Service {

    getWorkerStatistics(year, month) {
        return this.get('GetWorkerStatistic?', { year: year, month: month });
    }

    getServiceStatistics(year, month) {
        return this.get('GetServiceStatistic?', { year: year, month: month });
    }

}

export const reportService = new ReportService('/api/api/Report/');