import React from "react";
import { Dimmer, Loader } from "semantic-ui-react";

export default function LoadingComponent(prop) {
  return (
    <Dimmer active={true} inverted={prop.inverted || true}>
      <Loader content={prop.content || "Loading..."} />
    </Dimmer>
  );
}
