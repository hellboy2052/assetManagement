import { makeAutoObservable, reaction, runInAction } from "mobx";
import { stateOptions } from "../components/option/StateOption";
import consumer from "./consumer";
import { store } from "./store";

export default class AssetStore {
  assetRegistry = new Map();
  tableAsset = [];
  totalPages = 0;
  totalItems = 0;
  upperPageBound = 5;
  lowerPageBound = 0;
  assetPerPage = 10;
  filter = new Map()
    .set("currentIndex", 1)
    .set("Category", [])
    .set("States", [])
    .set("QueryString", "");
  loading = false;
  loadingInitial = false;
  tableFilter = new Map()
    .set("currentIndex", 1)
    .set("Category", [])
    .set("States", [1])
    .set("QueryString", "");

  constructor() {
    makeAutoObservable(this);

    reaction(
      () => this.filter.keys(),
      () => {
        this.assetRegistry.clear();
        this.loadAssets();
      }
    );

    reaction(
      () => this.totalItems,
      (totalItems) => {
        var lastRecord = this.totalPages * this.assetPerPage - totalItems;
        if (lastRecord < 0) {
          this.totalPages += 1;
        }

        if (lastRecord === this.assetPerPage) {
          this.totalPages -= 1;
          if (this.filter.get("currentIndex") !== 1) {
            this.filter.set(
              "currentIndex",
              this.filter.get("currentIndex") - 1
            );
          }
        }
      }
    );
  }

  increaseTotalItems = () => {
    this.totalItems = this.totalItems + 1;
  };

  reduceTotalItems = () => {
    this.totalItems = this.totalItems - 1;
  };

  setUpperBound = (n) => {
    this.upperPageBound = n;
  };
  setLowerBound = (n) => {
    this.lowerPageBound = n;
  };

  setIndex = (n) => {
    // Use to trigger reaction
    const resetPredicate = () => {
      this.filter.delete("currentIndex");
    };
    resetPredicate();
    this.filter.set("currentIndex", n);
  };

  get axiosParam() {
    const params = {};
    params["PageSize"] = this.assetPerPage;
    params["PageIndex"] = this.filter.get("currentIndex");
    this.filter.forEach((value, key) => {
      params[key] = value;
    });
    return params;
  }

  get tableAxiosParam() {
    const params = {};
    params["PageSize"] = this.assetPerPage;
    params["PageIndex"] = this.tableFilter.get("currentIndex");
    this.tableFilter.forEach((value, key) => {
      params[key] = value;
    });
    return params;
  }

  loadTableAssets = async () => {
    this.setLoadingInitial(true);
    try {
      const assets = await consumer.asset.filterList(this.tableAxiosParam);
      runInAction(() => {
        this.tableAsset = assets.assetReadModelList;
      });
      this.setLoadingInitial(false);
    } catch (err) {
      console.log(err);
      this.setLoadingInitial(false);
    }
  };

  get assetByNameSort() {
    return Array.from(this.assetRegistry.values()).splice(0, this.assetPerPage);
  }

  setTableSearchQuery = (search) => {
    const resetPredicate = () => {
      this.tableFilter.delete("QueryString");
    };
    resetPredicate();
    this.tableFilter.set("QueryString", search);
    this.tableFilter.set("currentIndex", 1);
    this.loadTableAssets();
  };

  LoadFilter = () => {
    this.setLoadingInitial(true);
    try {
      this.filter.set(
        "Category",
        Array.from(store.categoryStore.optionRegistry.values()).map(
          (c) => c.text
        )
      );
      this.filter.set("States", [...stateOptions.map((c) => c.value)]);
      this.setLoadingInitial(false);
    } catch (err) {
      console.log(err);
      this.setLoadingInitial(false);
    }
  };

  getAsset = (assetCode) => {
    return this.assetRegistry.get(assetCode);
  };

  loadAsset = async (assetCode) => {
    let asset = this.getAsset(assetCode);
    if (asset) {
      return asset;
    } else {
      this.setLoadingInitial(true);
      try {
        asset = await consumer.asset.detail(assetCode);
        this.setLoadingInitial(false);
        return asset;
      } catch (err) {
        console.log(err);
        this.setLoadingInitial(false);
      }
    }
  };

  loadAssetDetail = async (assetCode) => {
    try {
      var asset = await consumer.asset.detail(assetCode);
      if (asset) {
        return asset;
      }
      return null;
    } catch (err) {
      console.log(err);
    }
  };

  formattingHistroyDate = (histories) => {
    if (histories.length !== 0) {
      return [
        ...histories.map((history) => {
          return {
            ...history,
            assignedDate: this.formatDate(new Date(history.assignedDate)),
            returnedDate: this.formatDate(new Date(history.returnedDate)),
          };
        }),
      ];
    } else {
      return [];
    }
  };

  loadAssets = async () => {
    this.setLoadingInitial(true);
    try {
      const assets = await consumer.asset.filterList(this.axiosParam);
      assets.assetReadModelList.forEach((asset) => {
        this.setAsset(asset);
      });
      runInAction(() => {
        this.totalPages = assets.totalPages;
        this.totalItems = assets.totalItems;
      });
      this.setLoadingInitial(false);
    } catch (err) {
      console.log(err);
      this.setLoadingInitial(false);
    }
  };

  setCategoryFilter = (category) => {
    // Use to trigger reaction
    const resetPredicate = () => {
      this.filter.delete("Category");
    };
    resetPredicate();
    this.filter.set("Category", category);
    this.filter.set("currentIndex", 1);
  };
  setStateFilter = (state) => {
    const resetPredicate = () => {
      this.filter.delete("States");
    };
    resetPredicate();
    this.filter.set("States", state);
    this.filter.set("currentIndex", 1);
  };

  setSearchQuery = (search) => {
    // Use to trigger reaction
    const resetPredicate = () => {
      this.filter.delete("QueryString");
    };
    resetPredicate();
    this.filter.set("QueryString", search);
    this.filter.set("currentIndex", 1);
  };

  createAsset = async (assetFormValues) => {
    try {
      var formData = new FormData();
      formData.append("AssestName", assetFormValues.assestName);
      formData.append("CategoryId", assetFormValues.categoryId);
      formData.append(
        "InstallDate",
        this.formatDate(assetFormValues.installDate)
      );
      formData.append("Specification", assetFormValues.specification);
      formData.append("State", assetFormValues.state);
      const newAsset = await consumer.asset.create(formData);
      this.increaseTotalItems();
      if (this.assetByNameSort.length >= 1) {
        newAsset.installDate = this.formatDate(new Date(newAsset.installDate));
        var arr = this.assetByNameSort;
        arr.unshift(newAsset);
        runInAction(() => {
          this.assetRegistry.clear();
          this.assetRegistry = new Map(arr.map((i) => [i.assetCode, i]));
        });
      } else {
        this.setAsset(newAsset);
      }
    } catch (err) {
      console.log(err);
    }
  };

  updateAsset = async (assetFormValues) => {
    try {
      var formData = new FormData();
      formData.append("AssetCode", assetFormValues.assetCode);
      formData.append(
        "InstallDate",
        this.formatDate(assetFormValues.installDate)
      );
      formData.append("AssestName", assetFormValues.assestName);
      formData.append("Specification", assetFormValues.specification);
      formData.append("State", assetFormValues.state);
      var updatedAsset = await consumer.asset.update(formData);
      runInAction(() => {
        store.assignmentStore.loadAssignments();
        store.identityStore.loadAssigns();
        store.returnStore.loadReturns();
        if (updatedAsset.assetCode) {
          if (this.assetByNameSort.length >= 1) {
            updatedAsset.installDate = this.formatDate(
              new Date(updatedAsset.installDate)
            );
            var arr = this.assetByNameSort.filter(
              (i) => i.assetCode !== updatedAsset.assetCode
            );
            arr.unshift(updatedAsset);
            this.assetRegistry.clear();
            this.assetRegistry = new Map(arr.map((i) => [i.assetCode, i]));
          } else {
            this.setAsset(updatedAsset);
          }
        }
      });
    } catch (err) {
      throw err;
    }
  };

  updateAssetState = async (id, state) => {
    try {
      if (this.assetRegistry.size >= 1) {
        var asset = await this.loadAsset(id);
        asset.state = state;
        this.setAsset(asset);
      }
    } catch (err) {
      console.log(err);
    }
  };

  setAsset = (asset) => {
    asset.installDate = this.formatDate(new Date(asset.installDate));
    this.assetRegistry.set(asset.assetCode, asset);
  };

  deleteAsset = async (id) => {
    try {
      await consumer.asset.delete(id);
      this.reduceTotalItems();
      runInAction(() => {
        this.assetRegistry.delete(id);
        this.loadAssets();
        store.assignmentStore.assignmentRegistry.clear();
        store.assignmentStore.loadAssignments();
        store.modalStore.closeModal();
      });
    } catch (err) {
      console.log(err);
    }
  };

  setLoadingInitial = (state) => {
    this.loadingInitial = state;
  };

  setLoading = (state) => {
    this.loading = state;
  };

  formatDate(date) {
    return `${date.getFullYear()}-${date.getMonth() + 1}-${date.getDate()}`;
  }

  compare = (a, b) => {
    if (a.installDate < b.installDate) {
      return 1;
    }
    if (a.installDate > b.installDate) {
      return -1;
    }
    return 0;
  };
}
