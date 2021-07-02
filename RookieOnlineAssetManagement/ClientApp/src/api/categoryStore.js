import { makeAutoObservable, runInAction } from "mobx";
import consumer from "./consumer";
import { store } from "./store";

export default class CategoryStore {
  optionRegistry = new Map();
  loadingInitial = false;
  constructor() {
    makeAutoObservable(this);
  }

  get categoryOption() {
    return Array.from(this.optionRegistry.values());
  }

  loadCategories = async () => {
    this.setLoadingInitial(true);
    try {
      const categories = await consumer.category.list();
      categories.forEach((category) => {
        this.setCategory(category);
      });
      this.setLoadingInitial(false);
    } catch (error) {
      this.setLoadingInitial(false);
      console.log(error);
    }
  };

  setCategory = (category) => {
    this.optionRegistry.set(category.categoryId, {
      text: category.categoryName,
      value: category.categoryId,
    });
  };

  createCategory = async (data) => {
    try {
      var formData = new FormData();
      formData.append("CategoryName", data.CategoryName);
      const cate = await consumer.category.create(formData);
      runInAction(() => {
        this.setCategory(cate);
        store.assetStore.setCategoryFilter([
          ...store.assetStore.filter.get("Category"),
          data.CategoryName,
        ]);
        store.modalStore.closeModal();
      });
    } catch (err) {
      throw err
    }
  };

  setLoadingInitial = (state) => {
    this.loadingInitial = state;
  };
}
