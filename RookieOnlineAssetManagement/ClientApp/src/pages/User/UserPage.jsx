import { observer } from "mobx-react-lite";
import { useEffect } from "react";
import { useStore } from "../../api/store";
import UserFooter from "./UserFooter";
import UserHeader from "./UserHeader";
import UserTable from "./UserTable";

export default observer(function UserPage() {
  const { userStore, identityStore } = useStore();
  const { userRegistry, loadUsers } = userStore;
  const {setLocation} = identityStore;

  useEffect(() => {
    setLocation();
  }, [setLocation]);

  useEffect(() => {
    if (userRegistry.size <= 1) {
      loadUsers();
    }
  }, [userRegistry.size, loadUsers]);

  return (
    <>
      <h5 className="mb-4 page-title">User List</h5>
      <UserHeader />
      <UserTable />
      <UserFooter />
    </>
  );
});
