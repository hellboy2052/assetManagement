import { observer } from "mobx-react-lite";
import React from "react";
import {
  DropdownToggle,
  DropdownMenu,
  DropdownItem,
  UncontrolledDropdown,
} from "reactstrap";
import { useStore } from "../api/store";
import ChangePasswordModal from "../components/modal/ChangePasswordModal";
import ConfirmLogout from "../components/modal/ConfirmLogout";

function Navbar() {
  const { modalStore, identityStore } = useStore();
  const { account, currentLocation } = identityStore;
  return (
    <div>
      <nav
        className="navbar justify-content-between"
        style={{ backgroundColor: "#CF2338" }}
      >
        <div className="container">
          <a
            className="navbar-brand"
            style={{ color: "white", fontSize: "13px" }}
          >
            {currentLocation === "" && <strong>Home</strong>}
            {currentLocation === "users" && <strong>User</strong>}
            {currentLocation === "assets" && <strong>Asset</strong>}
            {currentLocation === "assignments" && <strong>Assignment</strong>}
            {currentLocation === "requests" && <strong>Request</strong>}
            {currentLocation === "report" && <strong>Report</strong>}
          </a>
          <UncontrolledDropdown>
            <DropdownToggle
              caret
              id="dropdownTog"
              style={{
                backgroundColor: "#CF2338",
                border: "none",
                fontSize: "13px",
              }}
            >
              <strong>{account.username}</strong>
            </DropdownToggle>
            <DropdownMenu>
              <DropdownItem
                onClick={() =>
                  modalStore.openModal(<ChangePasswordModal />, "sm")
                }
              >
                Change Password
              </DropdownItem>
              <DropdownItem divider />
              <DropdownItem
                onClick={() => modalStore.openModal(<ConfirmLogout />, "sm")}
              >
                Logout
              </DropdownItem>
            </DropdownMenu>
          </UncontrolledDropdown>
        </div>
      </nav>
    </div>
  );
}

export default observer(Navbar);
