import {worksheetService} from "../../services/WorksheetService";

export const worksheet = {
    state: {
        worksheet: {}
    },

    getters: {
        worksheet: state => state.worksheet
    },

    mutations: {
        setWorksheet: (state, worksheet) => state.worksheet = worksheet,
        addDetail: (state, detail) => state.worksheet.tetelek.push(detail)
    },

    actions: {
        saveWorksheet(context) {
            return worksheetService.setWorksheet(context.state.worksheet)
        },
        saveWorksheetDetails(context, detail) {
            return worksheetService.setWorksheetDetail(context.state.worksheet.id, detail)
        }
    }
};

/*{
    "id": 0,
    "szolgaltatas": {
        "id": 1
    },
    "mennyiseg": 2,
    "ar" : 14000,
    "rogzitette": "iroda"
}*/