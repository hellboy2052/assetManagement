import { useStore } from "../../api/store";
import { RequestTable } from "./RequestTable.jsx";
import { useEffect } from "react";
import { observer } from "mobx-react-lite";
import RequestHeader from "./RequestHeader";

export default observer(function RequestPage() {
  const { returnStore, identityStore } = useStore();
  const { loadReturns, returnRegistry } = returnStore;

  const { setLocation } = identityStore;

  useEffect(() => {
    setLocation();
  }, [setLocation]);
  
  useEffect(() => {
    if (returnRegistry.size <= 1) {
      loadReturns();
    }
  }, [loadReturns, returnRegistry.size]);
  return (
    <>
      <div>
        <h5 className="mb-4 page-title">Request List</h5>
        <RequestHeader />
        <RequestTable />
      </div>
    </>
  );
});
