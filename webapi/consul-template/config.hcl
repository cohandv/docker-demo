# This denotes the start of the configuration section for Consul. All values
# contained in this section pertain to Consul.
consul {
  address = "consul:8500"

  retry {
    enabled = true
    attempts = 12
    backoff = "125ms"
    max_backoff = "1m"
  }

}

max_stale = "1m"
log_level = "trace"

# This is the quiescence timers; it defines the minimum and maximum amount of
# time to wait for the cluster to reach a consistent state before rendering a
# template. This is useful to enable in systems that have a lot of flapping,
# because it will reduce the the number of times a template is rendered.
wait {
  min = "20s"
  max = "30s"
}

exec {
  command = "dotnet webapi.dll --urls http://0.0.0.0:5000"
  splay = "20s"
  kill_signal = "SIGKILL"
  kill_timeout = "10s"
}

# This block defines the configuration for a template. Unlike other blocks,
# this block may be specified multiple times to configure multiple templates.
# It is also possible to configure templates via the CLI directly.
template {
  source = "/app/publish/consul-info.ctmpl"
  destination = "/app/publish/consul-info.json"
  perms = 0777
  backup = true
  wait {
    min = "2s"
    max = "10s"
  }
}
