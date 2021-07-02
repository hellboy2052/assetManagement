import { observer } from "mobx-react-lite";
import { useEffect } from "react";
import { useStore } from "../../api/store";
import AssignmentFooter from "./AssignmentFooter";
import AssignmentHeader from "./AssignmentHeader";
import AssignmentTable from "./AssignmentTable";
export default observer(function AssignmentPage() {
  const { assignmentStore, identityStore } = useStore();
  const { assignmentRegistry, loadAssignments } = assignmentStore;

  const { setLocation } = identityStore;

  useEffect(() => {
    setLocation();
  }, [setLocation]);

  useEffect(() => {
    if (assignmentRegistry.size <= 1) {
      loadAssignments();
    }
  }, [assignmentRegistry.size, loadAssignments]);
  
  return (
    <>
      <div className="mb-4 page-title">
        <h5>Assignment List</h5>
      </div>
      <div>
        <AssignmentHeader />
        <AssignmentTable />
        <AssignmentFooter />
      </div>
    </>
  );
});
