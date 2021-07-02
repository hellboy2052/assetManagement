import { observer } from "mobx-react-lite";
import React from "react";
import { NavLink } from "react-router-dom";
import { Menu } from "semantic-ui-react";
import { useStore } from "../api/store";
import "./sidebar.css";

const style = {
  width: "90%",
  textAlign: "left",
  color: "black",
  marginTop: "2px",
};
function Sidebar() {
  const {
    identityStore: { account },
  } = useStore();
  return (
    <div
      className="list-group list-group-flush"
      style={{ marginRight: "5rem" }}
    >
      <div className="pb-4">
        <img
          src="/Logo_lk.png"
          alt="nashLogo"
          style={{ with: 65.5, height: 66 }}
        />
        <strong>
          <p style={{ color: "#CF2338", fontSize: "13px" }}>
            Online Asset Assignment
          </p>
        </strong>
      </div>
      <div style={{ fontSize: "14px" }}>
        <Menu vertical inverted style={{ background: "0 0" }}>
          <Menu.Item
            className="navitem"
            color="red"
            as={NavLink}
            exact
            to="/"
            style={style}
          >
            <strong>Home</strong>
          </Menu.Item>
          {account.role === "Admin" && (
            <>
              <Menu.Item
                className="navitem"
                color="red"
                as={NavLink}
                to="/users"
                style={style}
              >
                <strong>Manage User</strong>
              </Menu.Item>
              <Menu.Item
                className="navitem"
                color="red"
                as={NavLink}
                to="/assets"
                style={style}
              >
                <strong>Manage Asset</strong>
              </Menu.Item>
              <Menu.Item
                className="navitem"
                color="red"
                as={NavLink}
                to="/assignments"
                style={style}
              >
                <strong>Manage Assignment</strong>
              </Menu.Item>
              <Menu.Item
                className="navitem"
                color="red"
                as={NavLink}
                to="/requests"
                style={style}
              >
                <strong>Request for Returning</strong>
              </Menu.Item>
              <Menu.Item
                className="navitem"
                color="red"
                as={NavLink}
                to="/report"
                style={style}
              >
                <strong>Report</strong>
              </Menu.Item>
            </>
          )}
        </Menu>
      </div>
    </div>
  );
}

export default observer(Sidebar);
