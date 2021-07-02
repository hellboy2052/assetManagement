import { useField } from "formik";
import { observer } from "mobx-react-lite";
import React from "react";
import { Form, Label, Select, Grid } from "semantic-ui-react";

export default observer(function MySelectInput(prop) {
  const [field, meta, helpers] = useField(prop.name);
  return (
    <div className="mb-4">
      <Grid>
        <Grid.Column verticalAlign="middle" width={5}>
          <label>{prop.label}:</label>
        </Grid.Column>
        <Grid.Column verticalAlign="middle" width={11}>
          <Form.Field error={meta.touched && !!meta.error}>
            <Select
              clearable
              options={prop.options}
              value={field.value || null}
              onChange={(e, d) => helpers.setValue(d.value)}
              onBlur={() => helpers.setTouched(true)}
              placeholder={prop.placeholder}
              disabled={prop.disabled || false}
            />
          </Form.Field>
        </Grid.Column>
      </Grid>
      {meta.touched && meta.error ? (
        <Label
          basic
          color="red"
          style={{ marginTop: "10px", marginLeft: "122px" }}
        >
          {meta.error}
        </Label>
      ) : null}
    </div>
  );
});
