import { observer } from "mobx-react-lite";
import { useEffect } from "react";
import { Switch } from "react-router-dom";
import { Route } from "react-router";
import Sidebar from "./components/sidebar";
import Navbar from "./components/navbar";
import ModalContainer from "./components/ModalContainer";

import "./App.css";
import UserForm from "./pages/User/UserForm";
import { Container } from "semantic-ui-react";
import { useStore } from "./api/store";
import LoadingComponent from "./components/LoadingComponent";
import AdminRoute from "./components/AdminRoute";
import AssetForm from "./pages/Asset/AssetForm";
import AssignmentForm from "./pages/Assignment/AssignmentForm";
import HomePage from "./pages/Home/HomePage";
import UserPage from "./pages/User/UserPage";
import RequestPage from "./pages/Request/RequestPage";
import AssignmentPage from "./pages/Assignment/AssignmentPage";
import AssetPage from "./pages/Asset/AssetPage";
import ReportPage from "./pages/Report/ReportPage";
import ChangePasswordFirstLoginModal from "./components/modal/ChangePasswordFirstLoginModal";

function App() {
  const {
    modalStore,
    categoryStore: { optionRegistry, loadCategories },
    assetStore: { LoadFilter },
    identityStore: {
      isLoggedIn,
      setAccount,
      account,
      appLoaded,
      setAppLoaded,
      firstLogin,
    },
  } = useStore();

  useEffect(() => {
    if (account && account.role !== "Staff" && optionRegistry.size <= 0) {
      loadCategories().then(() => {
        LoadFilter();
      });
    }
  }, [optionRegistry.size, loadCategories, account, LoadFilter]);

  useEffect(() => {
    if (!isLoggedIn) {
      setAccount().finally(() => setAppLoaded());
    } else {
      setAppLoaded();
    }
  }, [isLoggedIn, setAccount, setAppLoaded]);

  if (!appLoaded || account == null) {
    return <LoadingComponent content="Loading App...." />;
  }

  if (account.isDefaultPassword) {
    firstLogin();
    modalStore.openModal(<ChangePasswordFirstLoginModal />, "sm", true);
  }
  return (
    <div className="App">
      <ModalContainer />
      <Navbar></Navbar>
      <div className="container pt-5">
        <div id="" className="d-flex">
          <Sidebar></Sidebar>
          <Container fluid style={{ marginTop: "2rem" }}>
            <Route exact path="/" component={HomePage} />
            <Route
              path="/(.+)"
              render={() => (
                <>
                  {/* Route switch from page to page */}
                  <Switch>
                    <AdminRoute exact path="/users" Component={UserPage} />
                    <AdminRoute
                      path={["/users/create", "/users/edit/:id"]}
                      Component={UserForm}
                    />
                    <AdminRoute exact path="/assets" Component={AssetPage} />
                    <AdminRoute
                      path={["/assets/create", "/assets/edit/:id"]}
                      Component={AssetForm}
                    />
                    <AdminRoute
                      exact
                      path="/assignments"
                      Component={AssignmentPage}
                    />
                    <AdminRoute
                      exact
                      path={["/assignments/create", "/assignments/edit/:id"]}
                      Component={AssignmentForm}
                    />
                    <AdminRoute path="/requests" Component={RequestPage} />
                    <AdminRoute path="/report" Component={ReportPage} />
                  </Switch>
                </>
              )}
            />
          </Container>
        </div>
      </div>
    </div>
  );
}

export default observer(App);
