import { Button } from "reactstrap";
import axios from "axios";
import { useEffect } from "react";
import ReportTable from "./ReportTable";
import { useStore } from "../../api/store";
import { observer } from "mobx-react-lite";

export default observer(function ReportPage() {
  const { reportStore, identityStore } = useStore();
  const { loadReports } = reportStore;

  const { setLocation } = identityStore;

  useEffect(() => {
    setLocation();
  }, [setLocation]);

  useEffect(() => {
    loadReports();
  }, [loadReports]);

  const handleClick = () => {
    const fileDownload = require("js-file-download");

    axios({
      url: "/api/reports/export",
      method: "GET",
      responseType: "blob", // Important
    }).then((response) => {
      fileDownload(response.data, "report.xlsx");
    });
  };
  return (
    <>
      <div>
        <h5 className="mb-4 page-title">Report</h5>
        <Button color="danger" style={{ float: "right" }} onClick={handleClick}>
          Export
        </Button>
      </div>
      <div>
        <ReportTable />
      </div>
    </>
  );
});
