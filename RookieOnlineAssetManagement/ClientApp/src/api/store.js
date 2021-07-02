import { createContext, useContext } from "react";
import AssetStore from "./assetStore";
import AssignmentStore from "./assignmentStore";
import CategoryStore from "./categoryStore";
import IdentityStore from "./identityStore";
import ModalStore from "./modalStore";
import UserStore from "./userStore";
import ReportStore from "./reportStore";
import ReturnStore from "./returnStore";

export const store = {
  userStore: new UserStore(),
  modalStore: new ModalStore(),
  identityStore: new IdentityStore(),
  categoryStore: new CategoryStore(),
  assetStore: new AssetStore(),
  reportStore: new ReportStore(),
  assignmentStore: new AssignmentStore(),
  returnStore: new ReturnStore(),
};

export const StoreContext = createContext(store);

export function useStore() {
  return useContext(StoreContext);
}
