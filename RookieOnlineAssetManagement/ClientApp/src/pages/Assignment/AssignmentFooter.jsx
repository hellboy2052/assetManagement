import { observer } from "mobx-react-lite";
import { useStore } from "../../api/store";
import React from "react";

export default observer(function AssignmentFooter() {
  const pagination = [];
  const {
    assignmentStore: {
      setIndex,
      totalPages,
      filter,
      upperPageBound,
      setUpperBound,
      lowerPageBound,
      setLowerBound,
    },
  } = useStore();

  for (var i = 1; i <= totalPages; i++) {
    pagination.push(i);
  }

  const renderPageNumber = pagination
    .map((number) => {
      if (number === 1 && filter.get("currentIndex") === 1) {
        return number;
      } else if (number < upperPageBound + 1 && number > lowerPageBound) {
        return number;
      }
    })
    .filter(Number);

  if (totalPages === 1 || totalPages === 0) {
    return null;
  }

  const setCurrentPage = (n, increment = -1, decrement = -1) => {
    setIndex(n);
    if (n === increment && n !== totalPages && n !== totalPages - 1) {
      setUpperBound(upperPageBound + 2);
      setLowerBound(lowerPageBound + 2);
    }

    if (n === decrement && n !== 1 && n !== 2) {
      setUpperBound(upperPageBound - 2);
      setLowerBound(lowerPageBound - 2);
    }
  };

  return (
    <div className="mt-5">
      <ul className="pagination">
        {!renderPageNumber.includes(1) && (
          <li
            key={1}
            className={
              1 === filter.get("currentIndex") ? "page-item active" : ""
            }
          >
            <a
              onClick={() => {
                if (1 !== filter.get("currentIndex")) {
                  setCurrentPage(1);
                  setUpperBound(5);
                  setLowerBound(0);
                }
              }}
              className="page-link"
            >
              1
            </a>
          </li>
        )}
        {pagination.length > 5 &&
          (!renderPageNumber.includes(2) || !renderPageNumber.includes(3)) && (
            <li key={2}>
              <a className="page-link">...</a>
            </li>
          )}
        {renderPageNumber.map((n) => (
          <li
            key={n}
            className={
              n === filter.get("currentIndex") ? "page-item active" : ""
            }
          >
            <a
              onClick={() => {
                if (n !== filter.get("currentIndex")) {
                  setCurrentPage(
                    n,
                    renderPageNumber[renderPageNumber.length - 1],
                    renderPageNumber[0]
                  );
                }
              }}
              className="page-link"
            >
              {n}
            </a>
          </li>
        ))}
        {pagination.length > 5 && !renderPageNumber.includes(totalPages - 1) && (
          <li key={totalPages - 1}>
            <a className="page-link">...</a>
          </li>
        )}
        {!renderPageNumber.includes(totalPages) && (
          <li
            key={totalPages}
            className={
              totalPages === filter.get("currentIndex")
                ? "page-item active"
                : ""
            }
          >
            <a
              onClick={() => {
                if (totalPages !== filter.get("currentIndex")) {
                  setCurrentPage(totalPages);
                  setUpperBound(totalPages);
                  setLowerBound(totalPages - 5);
                }
              }}
              className="page-link"
            >
              {totalPages}
            </a>
          </li>
        )}
      </ul>
    </div>
  );
});
