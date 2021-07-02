import { observer } from "mobx-react-lite";
import { useEffect } from "react";
import { useStore } from "../../api/store";
import AssetFooter from "./AssetFooter";
import AssetHeader from "./AssetHeader";
import AssetTable from "./AssetTable";

export default observer(function AssetPage() {
  const { assetStore, identityStore } = useStore();
  const { assetRegistry, loadAssets } = assetStore;

  const { setLocation } = identityStore;

  useEffect(() => {
    setLocation();
  }, [setLocation]);

  useEffect(() => {
    if (assetRegistry.size <= 1) {
      loadAssets();
    }
  }, [assetRegistry.size, loadAssets]);

  return (
    <>
      <h5 className="mb-4 page-title">Asset List</h5>
      <AssetHeader />
      <AssetTable />
      <AssetFooter />
    </>
  );
});
