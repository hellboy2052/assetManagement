import { makeAutoObservable } from "mobx";
import consumer from "./consumer";

export default class ReportStore {
    reportRegistry = new Map();
    loading = false;
    loadingInitial = false;
    rowPerPage = 20;

    constructor() {
        makeAutoObservable(this);
    }

    get listReport() {
        return Array.from(this.reportRegistry.values()).splice(0, this.rowPerPage);
    }

    loadReports = async () => {
        try {
            const reports = await consumer.report.list();
            reports.forEach((report) => {
                this.setRequest(report);
            });
        } catch (err) {
            console.log(err);
        }
    };

    setRequest = (report) => {
        this.reportRegistry.set(report.categoryName, report);
    }
}