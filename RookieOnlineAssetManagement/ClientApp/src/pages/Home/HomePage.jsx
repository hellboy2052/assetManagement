import { observer } from "mobx-react-lite";
import { useEffect } from "react";
import { useStore } from "../../api/store";
import { HomeTable } from "./HomeTable";

export default observer(function HomePage() {
  const { identityStore } = useStore();
  const { isLoggedIn, assignRegistry, loadAssigns, setLocation } =
    identityStore;

  useEffect(() => {
    setLocation();
  }, [setLocation]);
  useEffect(() => {
    if (isLoggedIn && assignRegistry.size <= 0) {
      loadAssigns();
    }
  }, [loadAssigns, isLoggedIn, assignRegistry.size]);
  return (
    <>
      <h5 className="mb-4 page-title">My Assignment</h5>
      <HomeTable />
    </>
  );
});
