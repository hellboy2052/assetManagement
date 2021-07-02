import React from "react";
import { Redirect, Route } from "react-router";
import { useStore } from "../api/store";

export default function AdminRoute({ Component, ...rest }) {
  const {
    identityStore: { account },
  } = useStore();
  return (
    <Route
      {...rest}
      render={(props) =>
        account.role === "Admin" ? (
          <Component {...props} />
        ) : (
          <Redirect to="/" />
        )
      }
    />
  );
}
